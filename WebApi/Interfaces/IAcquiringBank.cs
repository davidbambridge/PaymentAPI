using System.Threading.Tasks;

using Services.Common.DTO;

namespace WebApi.Interfaces
{
    /// <summary>
    /// Acquiring bank interface.
    /// </summary>
    public interface IAcquiringBank
    {
        /// <summary>
        /// Method for calling the processing bank.
        /// </summary>
        /// <param name="payment">The payment object</param>
        /// <returns>PaymentResult</returns>
        Task<PaymentResult> ProcessPaymentAsync(PaymentItem payment);
    }
}
