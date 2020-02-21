using WebApi.Data;

namespace WebApi.Interfaces
{
    /// <summary>
    /// Interface for db context
    /// </summary>
    public interface IPaymentControllerDbContext
    {
        /// <summary>
        /// Create a new context
        /// </summary>
        /// <returns></returns>
        PaymentControllerDbContext Create();
        
    }
}
