using FluentValidation;

using Services.Common.DTO;

namespace Services.Common.Validators
{
    /// <summary>
    /// Validates the contents of the card class
    /// </summary>
    public class CardValidator : AbstractValidator<Card>
    {
        /// <summary>
        /// Performs validation against the card class.
        /// </summary>
        public CardValidator()
        {
            RuleFor(x => x.CardNumber).NotEmpty().CreditCard();
            RuleFor(x => x.Cvv).NotEmpty().Must(x => x.ToString().Length == 3);
            RuleFor(x => x.Month).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().Must(x => x.ToString().Length == 4);
            RuleFor(x => x.NameOnCard).NotEmpty();
        }
    }
}
