using Services.Common.DTO;
using Services.Common.Validators;

using Xunit;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApiTests
{
    public class CardTests
    {
        [Fact]
        public void IsValidCard_HappyPath()
        {
            var validator = new CardValidator();

            ValidationResult result = validator.Validate(new Card()
            {
                CardNumber = "0000000000000000",
                Cvv = 123,
                Month = 1,
                Year = 2021,
                NameOnCard = "Mr D Smith"
            });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsInvalidCard_CardNumber_NotValid()
        {
            var validator = new CardValidator();

            ValidationResult result = validator.Validate(new Card()
            {
                CardNumber = "1000000000000000",
                Cvv = 123,
                Month = 1,
                Year = 2021,
                NameOnCard = "Mr D Smith"
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void IsInvalidCard_Cvv_ToShort()
        {
            var validator = new CardValidator();

            ValidationResult result = validator.Validate(new Card()
            {
                CardNumber = "0000000000000000",
                Cvv = 12,
                Month = 1,
                Year = 2021,
                NameOnCard = "Mr D Smith"
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void IsInvalidCard_Cvv_ToLong()
        {
            var validator = new CardValidator();

            ValidationResult result = validator.Validate(new Card()
            {
                CardNumber = "0000000000000000",
                Cvv = 12,
                Month = 1,
                Year = 2021,
                NameOnCard = "Mr D Smith"
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void IsInvalidCard_Year_InvalidFormat()
        {
            var validator = new CardValidator();

            ValidationResult result = validator.Validate(new Card()
            {
                CardNumber = "0000000000000000",
                Cvv = 122,
                Month = 1,
                Year = 21,
                NameOnCard = "Mr D Smith"
            });

            Assert.False(result.IsValid);
        }

    }
}
