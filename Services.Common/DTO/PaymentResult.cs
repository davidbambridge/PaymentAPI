using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Services.Common.DTO
{
    /// <summary>
    /// Simple class for returning the result of a payment to the client.
    /// </summary>
    public class PaymentResult
    {
        /// <summary>
        /// gets or sets the payment id.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the external payment references (not returned to client)
        /// </summary>
        [JsonIgnore]
        public string ExternalPaymentReference { get; set; }
    }
}
