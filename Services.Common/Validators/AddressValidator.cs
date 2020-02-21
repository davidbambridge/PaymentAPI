using FluentValidation;

using Services.Common.DTO;

namespace Services.Common.Validators
{
    /// <summary>
    /// Validates the contents of the address class
    /// </summary>
    public class AddressValidator : AbstractValidator<Address>
    {
        /// <summary>
        /// Performs validation against the address class.
        /// </summary>
        public AddressValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Postcode).NotEmpty();
            RuleFor(x => x.TownOrCity).NotEmpty();
        }
    }
}
