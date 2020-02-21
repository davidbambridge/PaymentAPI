using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

using WebApi.Interfaces;

using Services.Common.DTO;

namespace WebApi.Helpers
{
    /// <summary>
    /// Helper class for handling payment operations
    /// </summary>
    public class PaymentHelper : IPaymentHelper
    {
        private ILogger<IPaymentHelper> Logger { get; }
        private IAcquiringBank Bank { get; }
        private IDataAccess DataAccess { get; }

        /// <summary>
        /// Creates a new instance of the payment helper class.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="bank">Acquiring bank helper class</param>
        /// <param name="dataAccess">Data access helper class</param>
        public PaymentHelper(ILogger<IPaymentHelper> logger, IAcquiringBank bank, IDataAccess dataAccess)
        {
            Logger = logger;
            Bank = bank;
            DataAccess = dataAccess;
        }

        /// <summary>
        /// Method for processing payments
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <param name="payment">The payment details</param>
        /// <returns>PaymentResult object</returns>
        public async Task<PaymentResult> ProcessPaymentAsync(MerchantId merchantId, PaymentItem payment)
        { 
            Logger.LogInformation("Being processing payment for merchant {merchantId}", merchantId);

            payment.TransactionDateTime = DateTime.UtcNow;
            payment.PaymentMerchant = new Merchant() {MerchantId = merchantId };
            payment.PaymentStatus = PaymentStatus.Processing;

            PaymentResult paymentResult = await Bank.ProcessPaymentAsync(payment).ConfigureAwait(false);

            if (paymentResult is null)
            {
                Logger.LogInformation("Exception processing payment {PaymentId}", payment.PaymentId);
                throw new ArgumentOutOfRangeException(nameof(payment), "Payment was not processed by bank.");
            }

            Logger.LogInformation("Payment {PaymentId}, processed returned {PaymentStatus}", paymentResult.PaymentId, paymentResult.PaymentStatus);

            payment.ExternalPaymentReference = paymentResult.ExternalPaymentReference;
            payment.PaymentStatus = paymentResult.PaymentStatus;

            PaymentItem paymentItem = await DataAccess.SavePaymentItemAsync(payment).ConfigureAwait(false);

            if (paymentItem is null)
            {
                Logger.LogInformation("Exception saving payment {PaymentId}", payment.PaymentId);
                throw new ArgumentOutOfRangeException(nameof(paymentItem), "Payment details not save to data store.");
            }

            paymentResult.PaymentId = paymentItem.PaymentId.UUID;

            return paymentResult;
        }

        /// <summary>
        /// Gets the details of all payments made to a merchant
        /// </summary>
        /// <param name="merchantId">The merchant Id</param>
        /// <returns>List of payment items</returns>
        public async Task<List<PaymentItem>> GetPaymentsByMerchantIdAsync(MerchantId merchantId)
        {
            List<PaymentItem> paymentItems = await DataAccess.GetPaymentsByMerchantIdAsync(merchantId).ConfigureAwait(false);

            if (paymentItems.Any())
            {
                foreach (PaymentItem payment in paymentItems)
                {
                    payment.PaymentCard.CardNumber = "**** **** **** ****";
                    payment.PaymentCard.Cvv = 000;
                }
            }
  
            return paymentItems;
        }

        /// <summary>
        /// Gets the details of an individual payment 
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <param name="paymentId">The payment id</param>
        /// <returns>A paymentItem object</returns>
        public async Task<PaymentItem> GetPaymentByPaymentId(MerchantId merchantId, PaymentId paymentId)
        {
            PaymentItem payment = await DataAccess.GetPaymentByPaymentId(merchantId, paymentId).ConfigureAwait(false);
            payment.PaymentCard.CardNumber = "**** **** **** ****";
            payment.PaymentCard.Cvv = 000;

            return payment;
        }
    }
}
