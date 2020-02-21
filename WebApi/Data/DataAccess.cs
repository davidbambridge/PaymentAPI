using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Services.Common.DTO;

using WebApi.Extensions;
using WebApi.Interfaces;

using Model = Services.Common.Model;

namespace WebApi.Data
{
    /// <summary>
    /// Class for reading from and writing to data store.
    /// </summary>
    public class DataAccess : IDataAccess
    {
        private ILogger<IDataAccess> Logger { get; }
 
        private IPaymentControllerDbContext DbContextProvider { get; }

        /// <summary>
        /// Creates a new instance of the data access class.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="contextProvider">The db context provider class</param>
        public DataAccess(ILogger<IDataAccess> logger, IPaymentControllerDbContext contextProvider)
        {
            Logger = logger;
            DbContextProvider = contextProvider;
        }

        /// <summary>
        /// Method for creating a new customer.
        /// </summary>
        /// <param name="customer">The customer details</param>
        /// <returns>Customer object</returns>
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            Model.Customer dbCustomer = customer.ToDb();
            context.Customers.Add(dbCustomer);

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "An exception has occured in {name}", nameof(CreateCustomerAsync));
                Logger.LogInformation("Failed to create new customer {@customer}", customer);

                throw;
            }

            return dbCustomer.ToDto();
        }

        /// <summary>
        /// Method for saving payment details to database.
        /// </summary>
        /// <param name="payment">The payment</param>
        /// <returns>PaymentItem object</returns>
        public async Task<PaymentItem> SavePaymentItemAsync(PaymentItem payment)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            Model.PaymentItem dbPaymentItem = payment.ToDb();
            context.Payments.Add(dbPaymentItem);

            try
            {
                if (payment.PaymentCustomer.CustomerId.IsValid())
                {
                    context.Customers.Attach(dbPaymentItem.PaymentCustomer);
                }
                if (payment.PaymentCustomer.Address.AddressId.IsValid())
                {
                    context.Address.Attach(dbPaymentItem.PaymentCustomer.Address);
                }
                if (payment.PaymentCard.CardId.IsValid())
                {
                    context.Card.Attach(dbPaymentItem.PaymentCard);
                }

                context.Merchant.Attach(dbPaymentItem.PaymentMerchant);

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "An exception has occured in {name}", nameof(SavePaymentItemAsync));
                Logger.LogInformation("Payment {PaymentId} failed to process.", payment.PaymentId);

                throw;
            }

            return dbPaymentItem.ToDto();
        }

        /// <summary>
        /// Gets the details of a merchant
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>merchant object</returns>
        public async Task<Merchant> GetMerchantByIdAsync(MerchantId merchantId)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            Model.Merchant merchant = await context.Merchant.Where(x => x.MerchantId.Equals(merchantId.UUID)).SingleOrDefaultAsync();
            return merchant.ToDto();
        }

        /// <summary>
        /// Gets the details of all payments made to a merchant.
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>List of PaymentItems</returns>
        public async Task<List<PaymentItem>> GetPaymentsByMerchantIdAsync(MerchantId merchantId)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            List<Model.PaymentItem> payments = await context.Payments.Include(c => c.PaymentCard).Include(p => p.PaymentCustomer).Where(
                p => p.PaymentMerchant.Equals(new Model.Merchant() { MerchantId = merchantId.UUID })).ToListAsync().ConfigureAwait(false);

            return payments.ToDto();
        }

        /// <summary>
        /// Gets the details of a payment by Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<PaymentItem> GetPaymentByPaymentId(MerchantId merchantId, PaymentId paymentId)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            Model.PaymentItem payment = await context.Payments.Include(c => c.PaymentCard)
                .Include(p => p.PaymentCustomer).FirstOrDefaultAsync(
                p =>
                p.PaymentMerchant.Equals(new Model.Merchant() { MerchantId = merchantId.UUID }) &&
                p.PaymentId.Equals(paymentId.UUID)
                ).ConfigureAwait(false);

            return payment.ToDto();
        }

        /// <summary>
        /// Adds a new merchant account
        /// </summary>
        /// <param name="merchant">Details of new merchant</param>
        /// <returns>bool</returns>
        public async Task<bool> CreateMerchantAccountAsync(Merchant merchant)
        {
            await using PaymentControllerDbContext context = DbContextProvider.Create();

            try
            {
                Model.Merchant dbMerchant = merchant.ToDb();
                context.Merchant.Add(dbMerchant);

                int result = await context.SaveChangesAsync().ConfigureAwait(false);
                return result.Equals(1);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "An exception has occured in {name}", nameof(CreateMerchantAccountAsync));

                throw;
            }

        }
    }
}
