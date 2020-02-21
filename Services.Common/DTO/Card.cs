using FluentValidation;

using Newtonsoft.Json;

using Services.Common.Validators;

namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a payment card
    /// </summary>
    public class Card : ValidatedOptions<Card>
    {
        /// <summary>
        /// Gets or sets a cards id.
        /// </summary>
        [JsonIgnore]
        public CardId CardId { get; set; } = new CardId();

        /// <summary>
        /// Gets or sets the cards long number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the name as it appears on card
        /// </summary>
        public string NameOnCard { get; set; }

        /// <summary>
        /// Gets or sets 3 digit cvv number
        /// </summary>
        public int Cvv { get; set; }

        /// <summary>
        /// Gets or sets the expiry month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the expiry year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Base constructor for using the validator.
        /// </summary>
        public Card() : base(new CardValidator())
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