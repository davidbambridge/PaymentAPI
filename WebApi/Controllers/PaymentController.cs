using System;
using System.Collections.Generic;
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
    /// Controller for making and getting payments
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentHelper _paymentHelper;
        private readonly IMerchantHelper _merchantHelper;

        /// <summary>
        /// Initializes a new instance of the PaymentController class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="paymentHelper">payment helper class</param>
        /// <param name="merchantHelper">merchant helper class</param>
        public PaymentController(ILogger<PaymentController> logger, IPaymentHelper paymentHelper, IMerchantHelper merchantHelper)
        {
            _logger = logger;
            _paymentHelper = paymentHelper;
            _merchantHelper = merchantHelper;
        }

        /// <summary>
        /// Returns all payments made to a merchant by merchant id
        /// </summary>
        /// <param name="id">Id of merchant</param>
        /// <returns>List of payments</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentsByMerchantId(string id)
        {
            _logger.LogInformation($"Get payments for merchant {id}.", id);
            
            MerchantId merchantId = new MerchantId() { UUID = id };
            Merchant merchant = await _merchantHelper.GetMerchantByIdAsync(merchantId);

            if (merchant is null)
            {
                _logger.LogInformation("Payment processing failed, invalid merchant Id {id}", id);
                return new BadRequestObjectResult("Invalid merchant id");
            }

            List<PaymentItem> payments = await _paymentHelper.GetPaymentsByMerchantIdAsync(merchantId).ConfigureAwait(false);

            return new OkObjectResult(payments);
        }

        /// <summary>
        /// Gets an individual payment made to a merchant.
        /// </summary>
        /// <param name="id">Merchant Id</param>
        /// <param name="paymentId">Payment Id</param>
        /// <returns>A payment item</returns>
        [HttpGet("{id}/{paymentId}")]
        public async Task<IActionResult> GetPaymentByPaymentId(string id, string paymentId)
        {
            _logger.LogInformation($"Getting payment for merchant {id} by payment id {paymentId}.", id, paymentId);

            Merchant merchant = await CheckMerchantAsync(id).ConfigureAwait(false);

            if (merchant is null)
            {
                _logger.LogInformation("Payment processing failed, invalid merchant Id {id}", id);
                return new BadRequestObjectResult("Invalid merchant id");
            }

            PaymentItem payment =
                await _paymentHelper.GetPaymentByPaymentId(merchant.MerchantId, new PaymentId() {UUID = paymentId}).ConfigureAwait(false);

            return new OkObjectResult(payment);
        }

        /// <summary>
        /// Processes a new payment.
        /// </summary>
        /// <param name="id">Merchant id</param>
        /// <param name="payment">Payment object</param>
        /// <returns>Payment result</returns>
        [HttpPost("{id}")]
        [Produces(typeof(PaymentResult))]
        public async Task<IActionResult> ProcessPaymentAsync(string id, [FromBody] PaymentItem payment)
        {

            _logger.LogInformation($"Process payment {@payment} for merchant {id}.", payment, id);

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Failed to process payment {@payment}", payment);
                return new BadRequestObjectResult(ModelState);
            }

            Merchant merchant = await CheckMerchantAsync(id).ConfigureAwait(false);

            if (merchant is null)
            {
                _logger.LogInformation("Payment processing failed, invalid merchant Id {id}", id);
                return new BadRequestObjectResult("Invalid merchant id");
            }

            try
            {
                PaymentResult paymentResult = await _paymentHelper.ProcessPaymentAsync(merchant.MerchantId, payment)
                    .ConfigureAwait(false);

                _logger.LogInformation("Payment completed {PaymentId}.", paymentResult.PaymentId);

                return new CreatedResult(Request.GetRequestUri().ToString(), paymentResult);
            }
            catch (AggregateException aex)
            {
                _logger.LogWarning(aex, "Exception creating merchant {@merchant}", merchant);
                IActionResult handled = ExceptionExtensions.HandleException(aex);

                if (handled != null)
                {
                    return handled;
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Exception creating merchant {@merchant}", merchant);
                throw;
            }
        }

        private async Task<Merchant> CheckMerchantAsync(string id)
        {
            MerchantId merchantId = new MerchantId() {UUID = id};
            return await _merchantHelper.GetMerchantByIdAsync(merchantId).ConfigureAwait(false);
        }
    }

}