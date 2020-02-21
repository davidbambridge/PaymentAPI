using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Services.Common.DTO;

using WebApi.Interfaces;

namespace WebApi.Helpers
{
    /// <summary>
    /// Helper class for calling acquiring bank
    /// </summary>
    public class AcquiringBank : IAcquiringBank
    {
        private readonly HttpClient _client;
        private ILogger<IAcquiringBank> _logger { get; }

        /// <summary>
        /// Creates a new instance of the AcquiringBank class
        /// </summary>
        /// <param name="client">the HttpClient</param>
        /// <param name="logger">the logger</param>
        public AcquiringBank(HttpClient client, ILogger<IAcquiringBank> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Method for calling the processing bank.
        /// </summary>
        /// <param name="payment">The payment object</param>
        /// <returns>PaymentResult</returns>
        public async Task<PaymentResult> ProcessPaymentAsync(PaymentItem payment)
        {
            PaymentResult result = new PaymentResult();

            try
            {
                var newPayment = new
                {
                    Card = payment.PaymentCard,
                    Amount = payment.PaymentAmount,
                    PaymentCurrency = payment.PaymentCurrency.ToString()
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(newPayment), Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await _client.PostAsync("payment", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var definition = new {PaymentSucceeded = true, PaymentId = ""};
                    var responseJson = JsonConvert.DeserializeAnonymousType(responseMessage, definition);

                    result.PaymentStatus =
                        responseJson.PaymentSucceeded ? PaymentStatus.Approved : PaymentStatus.Declined;

                    result.ExternalPaymentReference = responseJson.PaymentId;
                    _logger.LogInformation(
                        "Payment processed successfully for {PaymentId}, Response - StatusCode: {StatusCode}",
                        payment.PaymentId, response.StatusCode);
                }
                else
                {
                    _logger.LogInformation(
                        "Processing payment failed for {PaymentId}, Response - StatusCode: {StatusCode}",
                        payment.PaymentId, response.StatusCode);
                    result.PaymentStatus = PaymentStatus.Failed;
                }
            }
            catch (Exception ex)
            {
                result.PaymentStatus = PaymentStatus.Failed;
                _logger.LogWarning(ex, "An exception has occured in {name}", nameof(ProcessPaymentAsync));
            }

            return result;

        }
    }
}
