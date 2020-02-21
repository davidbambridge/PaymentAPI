using System.Threading.Tasks;

using Services.Common.DTO;

namespace WebApi.Interfaces
{
    /// <summary>
    /// Merchant helper interface
    /// </summary>
    public interface IMerchantHelper
    {
        /// <summary>
        /// Get the details of a merchant by id
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>A merchant</returns>
        Task<Merchant> GetMerchantByIdAsync(MerchantId merchantId);

        /// <summary>
        /// Creates a new merchant in the system.
        /// </summary>
        /// <returns>A new merchant object</returns>
        Task<Merchant> CreateNewMerchantAsync();
    }
}
