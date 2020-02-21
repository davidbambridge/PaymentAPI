using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common.DTO;

namespace WebApi.Interfaces
{
    /// <summary>
    /// The payment helper interface 
    /// </summary>
    public interface IPaymentHelper
    {
        /// <summary>
        /// Method for processing payments
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <param name="payment">The payment details</param>
        /// <returns>PaymentResult object</returns>
        Task<PaymentResult> ProcessPaymentAsync(MerchantId merchantId, PaymentItem payment);

        /// <summary>
        /// Gets the details of all payments made to a merchant
        /// </summary>
        /// <param name="merchantId">The merchant Id</param>
        /// <returns>List of payment items</returns>
        Task<List<PaymentItem>> GetPaymentsByMerchantIdAsync(MerchantId merchantId);

        /// <summary>
        /// Gets the details of an individual payment 
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <param name="paymentId">The payment id</param>
        /// <returns>A paymentItem object</returns>
        Task<PaymentItem> GetPaymentByPaymentId(MerchantId merchantId, PaymentId paymentId);

    }
}
