using FluentValidation;

using Services.Common.DTO;

namespace Services.Common.Validators
{
    /// <summary>
    /// Validates the contents of the customer class
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        /// <summary>
        /// Performs validation against the customer class.
        /// </summary>
        public CustomerValidator()
        {
            RuleFor(x => x.Title).IsInEnum();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FamilyName).NotEmpty();
        }
    }
}
