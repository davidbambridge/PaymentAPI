using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Common.DTO;
using Services.Common.Extensions;
using WebApi.Extensions;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for interacting with merchant accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly ILogger<MerchantController> _logger;
        private readonly IMerchantHelper _merchantHelper;

        /// <summary>
        /// Initializes a new instance of the MerchantController class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="merchantHelper">The merchant helper class</param>
        public MerchantController(ILogger<MerchantController> logger, IMerchantHelper merchantHelper)
        {
            _logger = logger;
            _merchantHelper = merchantHelper;
        }

        /// <summary>
        /// Creates a new merchant.
        /// </summary>
        /// <returns>Merchant object</returns>
        [HttpPost]
        [Produces(typeof(Merchant))]
        public async Task<IActionResult> CreateMerchantAccount()
        {
            _logger.LogInformation($"Creating new merchant account");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Failed to create merchant account");
                return new BadRequestObjectResult(ModelState);
            }

            Merchant merchant = new Merchant();

            try
            {
                merchant = await _merchantHelper.CreateNewMerchantAsync().ConfigureAwait(false);
            }
            catch (AggregateException aex)
            {
                _logger.LogWarning(aex, "Exception creating merchant {@merchant}", merchant);
                IActionResult handled = ExceptionExtensions.HandleException(aex);

                if (handled != null)
                {
                    return handled;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Exception creating merchant {@merchant}", merchant);
                throw;
            }

            return new CreatedResult(Request.GetRequestUri() + "/" + merchant.MerchantId, merchant);
        }
    }
}