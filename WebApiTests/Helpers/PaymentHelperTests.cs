using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Common.DTO;
using WebApi.Interfaces;
using Xunit;

namespace WebApi.Helpers.Tests
{
    public class PaymentHelperTests
    {

        private readonly Mock<ILogger<IPaymentHelper>> _mockLogger;
        private readonly Mock<IAcquiringBank> _mockBank;
        private readonly Mock<IDataAccess> _mockDataAccess;

        public PaymentHelperTests()
        {
            _mockLogger = new Mock<ILogger<IPaymentHelper>>();
            _mockBank = new Mock<IAcquiringBank>();
            _mockDataAccess = new Mock<IDataAccess>();
        }

        [Fact]
        public async Task ProcessPayment_HappyPath()
        {

            PaymentItem payment = new PaymentItem()
            {
                PaymentAmount = 3.50,
                PaymentCurrency = PaymentCurrency.Gbp,
                PaymentCard = new Card()
                {
                    CardNumber = "4462918603783343",
                    Cvv = 111,
                    NameOnCard = "Mr D P Bambridge",
                    Month = 10,
                    Year = 2022
                },
                PaymentMerchant = new Merchant() { MerchantId = new MerchantId() {UUID = Guid.NewGuid().ToString() }},
                PaymentCustomer = new Customer()
                {
                    Title = Title.Mr,
                    FirstName = "David",
                    FamilyName = "Bambridge",
                    Address = new Address()
                    {
                        AddressLine1 = "41 St Georges Park Avenue",
                        TownOrCity = "Westcliff-on-Sea",
                        Country = "United Kingdom",
                        Postcode = "SS0 9UE"
                    }
                }
            };

            MerchantId merchantId = new MerchantId() { UUID = Guid.NewGuid().ToString() };

            PaymentHelper paymentHelper = new PaymentHelper(_mockLogger.Object, _mockBank.Object, _mockDataAccess.Object);
            PaymentResult paymentResult = await paymentHelper.ProcessPaymentAsync(merchantId, payment).ConfigureAwait(false);
           


            Assert.True(true);
        }
    }
}