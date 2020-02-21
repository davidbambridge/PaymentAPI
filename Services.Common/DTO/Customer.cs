using FluentValidation;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Services.Common.Validators;

namespace Services.Common.DTO
{

    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer : ValidatedOptions<Customer>
    {
        /// <summary>
        /// Gets or sets the id of the customer
        /// </summary>
        public CustomerId CustomerId { get; set; } = new CustomerId();
        
        /// <summary>
        /// Gets or sets the customers title, i.e. Mr, Mrs, Dr etc.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Title Title { get; set; }

        /// <summary>
        /// Gets of sets the customers first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the customers family name (surname)
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the customers address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Base constructor for using the validator.
        /// </summary>
        public Customer() : base(new CustomerValidator())
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
