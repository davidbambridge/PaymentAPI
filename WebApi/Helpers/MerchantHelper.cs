using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Services.Common.DTO;

using WebApi.Interfaces;

namespace WebApi.Helpers
{
    /// <summary>
    /// Helper class for merchants
    /// </summary>
    public class MerchantHelper : IMerchantHelper
    {
        private ILogger<IMerchantHelper> Logger { get; }
   
        private IDataAccess DataAccess { get; }

        /// <summary>
        /// Creates a new instance of the merchant helper class
        /// </summary>
        /// <param name="logger">the logger</param>
        /// <param name="dataAccess">data access helper class</param>
        public MerchantHelper(ILogger<IMerchantHelper> logger, IDataAccess dataAccess)
        {
            Logger = logger;
            DataAccess = dataAccess;
        }

        /// <summary>
        /// Get the details of a merchant by id
        /// </summary>
        /// <param name="merchantId">The merchant id</param>
        /// <returns>A merchant</returns>
        public async Task<Merchant> GetMerchantByIdAsync(MerchantId merchantId)
        {
            return await DataAccess.GetMerchantByIdAsync(merchantId).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new merchant in the system.
        /// </summary>
        /// <returns>A new merchant object</returns>
        public async Task<Merchant> CreateNewMerchantAsync()
        {
            Merchant merchant = new Merchant() {MerchantId = new MerchantId() {UUID = Guid.NewGuid().ToString()}};

            Logger.LogInformation("Being creating new merchant {merchantId}", merchant.MerchantId);

            bool created = await DataAccess.CreateMerchantAccountAsync(merchant).ConfigureAwait(false);

            if (created) return merchant;

            Logger.LogInformation("Exception creating new merchant {@merchant}", merchant);
            throw new ArgumentOutOfRangeException(nameof(created), "There was a problem creating a new merchant.");

        }
    }
}
