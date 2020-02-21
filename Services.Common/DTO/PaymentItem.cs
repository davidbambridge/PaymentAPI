using System;

using FluentValidation;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Services.Common.Validators;

namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a payment
    /// </summary>
    public class PaymentItem : ValidatedOptions<PaymentItem>
    {

        /// <summary>
        /// Gets or sets the payment Id
        /// </summary>
        [JsonProperty("paymentId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PaymentId PaymentId { get; set; } = new PaymentId();

        /// <summary>
        /// Gets or sets the merchant being paid
        /// </summary>
        [JsonProperty("paymentMerchant", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Merchant PaymentMerchant { get; set; }

        /// <summary>
        /// Gets or sets the customer making the payment
        /// </summary>
        [JsonProperty("paymentCustomer")]
        public Customer PaymentCustomer { get; set; }

        /// <summary>
        /// Gets or sets the details of the payment card.
        /// </summary>
        [JsonProperty("paymentCard")]
        public Card PaymentCard { get; set; }

        /// <summary>
        /// Gets or sets the amount being paid.
        /// </summary>
        [JsonProperty("paymentAmount")]
        public double PaymentAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency in which the payment is being made.
        /// </summary>
        [JsonProperty("paymentCurrency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentCurrency PaymentCurrency { get; set; }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        [JsonProperty("paymentStatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Submitted;

        /// <summary>
        /// Gets or sets the payment reference received from the acquiring bank.
        /// </summary>
        [JsonProperty("externalPaymentReference", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ExternalPaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the dat and time of the transaction
        /// </summary>
        [JsonProperty("transactionDateTime")]
        public DateTime TransactionDateTime { get; set; }

        /// <summary>
        /// Base constructor for using the validator.
        /// </summary>
        public PaymentItem() : base(new PaymentItemValidator())
        { }

        /// <summary>
        /// Method for validating content of object.
        /// </summary>
        public override void ValidateAndThrow()
        {
            OptionsValidator.ValidateAndThrow(this);
        }
    }
}
