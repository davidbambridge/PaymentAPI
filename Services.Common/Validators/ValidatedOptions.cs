using FluentValidation;

namespace Services.Common.Validators
{
    /// <summary>
    /// Base Validated Configuration Options class
    /// </summary>
    /// <typeparam name="T">Option class type to validate</typeparam>
    public abstract class ValidatedOptions<T>
    {
        /// <summary>
        /// Associated validator
        /// </summary>
        protected readonly IValidator<T> OptionsValidator;

        /// <summary>
        /// Base class constructor
        /// </summary>
        /// <param name="optionsValidator">Options class validator to use</param>
        protected ValidatedOptions(IValidator<T> optionsValidator)
        {
            OptionsValidator = optionsValidator;
        }

        /// <summary>
        /// Performs validation and then throws an exception if validation fails
        /// </summary>
        public abstract void ValidateAndThrow();
    }
}
