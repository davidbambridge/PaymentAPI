using System.Collections.Generic;
using System.Threading.Tasks;

using Services.Common.DTO;

namespace WebApi.Interfaces
{
    /// <summary>
    /// Data access interface
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Method for creating a new customer.
        /// </summary>
        /// <param name="customer">The customer details</param>
        /// <returns>Customer object</returns>
        Task<Customer> CreateCustomerAsync(Customer customer);

        /// <summary>
        /// Method for saving payment details to database.
        /// </summary>
        /// <param name="payment">The payment</param>
        /// <returns>PaymentItem object</returns>
        Task<PaymentItem> SavePaymentItemAsync(PaymentItem payment);

        /// <summary>
        /// Gets the details of a merchant
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>merchant object</returns>
        Task<Merchant> GetMerchantByIdAsync(MerchantId merchantId);

        /// <summary>
        /// Gets the details of all payments made to a merchant.
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>List of PaymentItems</returns>
        Task<List<PaymentItem>> GetPaymentsByMerchantIdAsync(MerchantId merchantId);

        /// <summary>
        /// Gets the details of a payment by Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<PaymentItem> GetPaymentByPaymentId(MerchantId merchantId, PaymentId paymentId);

        /// <summary>
        /// Adds a new merchant account
        /// </summary>
        /// <param name="merchant">Details of new merchant</param>
        /// <returns>bool</returns>
        Task<bool> CreateMerchantAccountAsync(Merchant merchant);

    }
}
