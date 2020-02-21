using System;
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
            PaymentResult paymentResult = new PaymentResult()
            {
                ExternalPaymentReference = Guid.NewGuid().ToString(),
                PaymentStatus = PaymentStatus.Approved,
                PaymentId = Guid.NewGuid().ToString()
            };


            PaymentItem payment = CreateValidPaymentItem();

            _mockBank.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentItem>())).ReturnsAsync(paymentResult);
            _mockDataAccess.Setup(x => x.SavePaymentItemAsync(It.IsAny<PaymentItem>())).ReturnsAsync(payment);

            MerchantId merchantId = new MerchantId() { UUID = Guid.NewGuid().ToString() };

            PaymentHelper paymentHelper = new PaymentHelper(_mockLogger.Object, _mockBank.Object, _mockDataAccess.Object);
            PaymentResult result = await paymentHelper.ProcessPaymentAsync(merchantId, payment).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProcessPayment_BankError()
        {

            PaymentItem payment = CreateValidPaymentItem();
            _mockBank.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentItem>())).ReturnsAsync((PaymentResult)null);
           
            MerchantId merchantId = new MerchantId() { UUID = Guid.NewGuid().ToString() };

            PaymentHelper paymentHelper = new PaymentHelper(_mockLogger.Object, _mockBank.Object, _mockDataAccess.Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                 paymentHelper.ProcessPaymentAsync(merchantId, payment)).ConfigureAwait(false);
        }


        [Fact]
        public async Task ProcessPayment_DataStoreError()
        {
            PaymentResult paymentResult = new PaymentResult()
            {
                ExternalPaymentReference = Guid.NewGuid().ToString(),
                PaymentStatus = PaymentStatus.Approved,
                PaymentId = Guid.NewGuid().ToString()
            };

            PaymentItem payment = CreateValidPaymentItem();
            _mockBank.Setup(x => x.ProcessPaymentAsync(It.IsAny<PaymentItem>())).ReturnsAsync(paymentResult);
            _mockDataAccess.Setup(x => x.SavePaymentItemAsync(It.IsAny<PaymentItem>())).ReturnsAsync((PaymentItem)null);


            MerchantId merchantId = new MerchantId() { UUID = Guid.NewGuid().ToString() };

            PaymentHelper paymentHelper = new PaymentHelper(_mockLogger.Object, _mockBank.Object, _mockDataAccess.Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                 paymentHelper.ProcessPaymentAsync(merchantId, payment)).ConfigureAwait(false);
        }

        private PaymentItem CreateValidPaymentItem()
        {
            return new PaymentItem()
            {
                PaymentAmount = 3.50,
                PaymentCurrency = PaymentCurrency.Gbp,
                PaymentCard = new Card()
                {
                    CardNumber = "0000000000000000",
                    Cvv = 111,
                    NameOnCard = "Mr D P Bambridge",
                    Month = 10,
                    Year = 2022
                },
                PaymentMerchant = new Merchant() { MerchantId = new MerchantId() { UUID = Guid.NewGuid().ToString() } },
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
        }

    }
}