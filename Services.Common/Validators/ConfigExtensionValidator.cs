using FluentValidation;

using Services.Common.Extensions;

namespace Services.Common.Validators
{
    /// <summary>
    /// Validates the contents of the ConfigExtension class
    /// </summary>
    public class ConfigExtensionValidator : AbstractValidator<ConfigExtension>
    {
        /// <summary>
        /// Performs validation against the ConfigExtension class.
        /// </summary>
        public ConfigExtensionValidator()
        {
            RuleFor(x => x.AcquiringBankUri).NotEmpty();
        }
    }
}

