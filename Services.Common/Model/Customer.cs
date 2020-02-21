using System.ComponentModel.DataAnnotations;


namespace Services.Common.Model
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        [Key]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customers title, i.e. Mr, Mrs, Dr etc.
        /// </summary>
        [Required]
        public int Title { get; set; }

        /// <summary>
        /// Gets of sets the customers first name
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string FistName { get; set; }

        /// <summary>
        /// Gets or sets the customers family name (surname)
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the customers address
        /// </summary>
        public Address Address { get; set; }
    }
}
