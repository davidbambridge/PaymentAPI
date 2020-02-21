using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Services.Common.DTO;

using WebApi.Helpers;
using WebApi.Interfaces;

using Xunit;

namespace WebApiTests.Helpers
{
    public class MerchantHelperTests
    {
        private readonly Mock<ILogger<IMerchantHelper>> _mockLogger;
        private readonly Mock<IDataAccess> _mockDataAccess;

        public MerchantHelperTests()
        {
            _mockDataAccess = new Mock<IDataAccess>();
            _mockLogger = new Mock<ILogger<IMerchantHelper>>();
        }

        [Fact]
        public async Task CreateNewMerchant_HappyPath()
        {
            MerchantHelper merchantHelper = new MerchantHelper(_mockLogger.Object, _mockDataAccess.Object);

            _mockDataAccess.Setup(x => x.CreateMerchantAccountAsync(It.IsAny<Merchant>())).ReturnsAsync(true);

            Merchant result = await merchantHelper.CreateNewMerchantAsync().ConfigureAwait(false);

            Assert.True(result.MerchantId.IsValid());
        }


        [Fact]
        public async Task CreateNewMerchant_ThrowException()
        {
            MerchantHelper merchantHelper = new MerchantHelper(_mockLogger.Object, _mockDataAccess.Object);

            _mockDataAccess.Setup(x => x.CreateMerchantAccountAsync(It.IsAny<Merchant>())).ReturnsAsync(false);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                  merchantHelper.CreateNewMerchantAsync()).ConfigureAwait(false);
        }

        [Fact]
        public async Task GetMerchant_HappyPath()
        {
            MerchantHelper merchantHelper = new MerchantHelper(_mockLogger.Object, _mockDataAccess.Object);
            MerchantId merchantId = new MerchantId() {UUID = Guid.NewGuid().ToString() };
            Merchant merchant = new Merchant()
            {
                MerchantId = merchantId
            };

            _mockDataAccess.Setup(x => x.GetMerchantByIdAsync(It.IsAny<MerchantId>())).ReturnsAsync(merchant);

            Merchant result = await merchantHelper.GetMerchantByIdAsync(merchantId).ConfigureAwait(false);

            Assert.Equal(merchant, result);
        }
    }
}
