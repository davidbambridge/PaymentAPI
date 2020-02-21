using FluentValidation;

using Services.Common.DTO;

namespace Services.Common.Validators
{
    /// <summary>
    /// Validates the contents of the paymentItem class
    /// </summary>
    public class PaymentItemValidator : AbstractValidator<PaymentItem>
    {
        /// <summary>
        /// Performs validation against the PaymentItem class.
        /// </summary>
        public PaymentItemValidator()
        {
            RuleFor(x => x.PaymentAmount).NotEmpty();
            RuleFor(x => x.PaymentCard).NotEmpty().SetValidator(new CardValidator());
            RuleFor(x => x.PaymentCurrency).IsInEnum();
            RuleFor(x => x.PaymentCustomer).NotEmpty().SetValidator(new CustomerValidator());
        }
    }
}
