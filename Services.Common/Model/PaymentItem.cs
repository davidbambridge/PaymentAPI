using System;
using System.ComponentModel.DataAnnotations;


namespace Services.Common.Model
{
    /// <summary>
    /// Represents a payment
    /// </summary>
    public class PaymentItem
    {
        /// <summary>
        /// Gets or sets the payment Id
        /// </summary>
        [Key]
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the merchant being paid
        /// </summary>
        [Required]
        [MaxLength(256)]
        public Merchant PaymentMerchant { get; set; }

        /// <summary>
        /// Gets or sets the customer making the payment
        /// </summary>
        [Required]
        public Customer PaymentCustomer { get; set; }

        /// <summary>
        /// Gets or sets the details of the payment card.
        /// </summary>
        [Required]
        public Card PaymentCard { get; set; }

        /// <summary>
        /// Gets or sets the amount being paid.
        /// </summary>
        [Required]
        public double PaymentAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency in which the payment is being made.
        /// </summary>
        [Required]
        public int PaymentCurrency { get; set; }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        [Required]
        public int PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the payment reference received from the acquiring bank.
        /// </summary>
        [MaxLength(256)]
        public string ExternalPaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the dat and time of the transaction
        /// </summary>
        [Required]
        public DateTime TransactionDateTime { get; set; }
    }
}
