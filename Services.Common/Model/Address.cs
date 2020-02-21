using System.ComponentModel.DataAnnotations;

namespace Services.Common.Model
{
    /// <summary>
    /// Represents a physical address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the id of the address
        /// </summary>
        [Key]
        public string AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        [MaxLength(256)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the town or city.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string TownOrCity { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}
