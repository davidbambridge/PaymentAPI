using FluentValidation;

using Services.Common.Validators;

namespace Services.Common.Extensions
{
    /// <summary>
    /// Class for app configuration values
    /// </summary>
    public class ConfigExtension : ValidatedOptions<ConfigExtension>
    {
        /// <summary>
        /// Gets or sets the uri of the bank that will process the transaction. 
        /// </summary>
        public string AcquiringBankUri { get; set; }

        /// <summary>
        /// Base constructor for using the validator.
        /// </summary>
        public ConfigExtension() : base(new ConfigExtensionValidator())
        {
        }

        /// <summary>
        /// Method for validating content of object.
        /// </summary>
        public override void ValidateAndThrow()
        {
            OptionsValidator.ValidateAndThrow(this);
        }
    }
}
