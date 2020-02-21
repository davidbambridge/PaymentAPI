using System.ComponentModel.DataAnnotations;

namespace Services.Common.Model
{
    /// <summary>
    /// Represents a payment card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets a cards id.
        /// </summary>
        [Key]
        public string CardId { get; set; }

        /// <summary>
        /// Gets or sets the cards long number
        /// </summary>
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the name as it appears on card
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string NameOnCard { get; set; }

        /// <summary>
        /// Gets or sets 3 digit cvv number
        /// </summary>
        [Required]
        public int Cvv { get; set; }

        /// <summary>
        /// Gets or sets the expiry month
        /// </summary>
        [Required]
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the expiry year
        /// </summary>
        [Required]
        public int Year { get; set; }
    }
}
