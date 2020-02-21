using FluentValidation;
using Services.Common.Validators;

namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a physical address
    /// </summary>
    public class Address : ValidatedOptions<Address>
    {
        /// <summary>
        /// Gets or sets the id of the address
        /// </summary>
        public AddressId AddressId { get; set; } = new AddressId();

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the town or city.
        /// </summary>
        public string TownOrCity { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Base constructor for using the validator.
        /// </summary>
        public Address() : base(new AddressValidator())
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
