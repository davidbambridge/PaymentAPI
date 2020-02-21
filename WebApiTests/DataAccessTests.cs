using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using DTO = Services.Common.DTO;
using Services.Common.Model;

using WebApi.Data;
using WebApi.Interfaces;
using Xunit;

using MockQueryable.Moq;

namespace WebApiTests
{
    public class DataAccessTests
    {
        private readonly Mock<ILogger<IDataAccess>> _mockLogger;
        private string _merchantId;

        public DataAccessTests()
        {
            _mockLogger = new Mock<ILogger<IDataAccess>>();
            _merchantId = Guid.NewGuid().ToString();
        }

        private IPaymentControllerDbContext CreateDbContext()
        {
            // Create a mock implementation of the DbContext and it's provider
            Mock<IPaymentControllerDbContext> mockProvider = new Mock<IPaymentControllerDbContext>();

            Mock<PaymentControllerDbContext> mockContext = new Mock<PaymentControllerDbContext>();

            var data = new List<Merchant>
            {
                new Merchant { MerchantId = _merchantId },
                new Merchant { MerchantId = Guid.NewGuid().ToString() },
                new Merchant { MerchantId = Guid.NewGuid().ToString() }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Merchant>>();
            mockSet.As<IQueryable<Merchant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Merchant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Merchant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Merchant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Merchant).Returns(mockSet.Object);
            mockProvider.Setup(p => p.Create()).Returns(mockContext.Object);
            return mockProvider.Object;
        }

        [Fact]
        public async Task GetMerchantByIdAsync_HappyPath()
        {
            DataAccess dataAccess = new DataAccess(_mockLogger.Object, CreateDbContext());
            DTO.Merchant merchant = await dataAccess.GetMerchantByIdAsync(new DTO.MerchantId() { UUID = _merchantId }).ConfigureAwait(false);

            Assert.NotNull(merchant);
        }

        [Fact]
        public async Task GetMerchantByIdAsync_MerchantNotFound()
        {
            DataAccess dataAccess = new DataAccess(_mockLogger.Object, CreateDbContext());
            DTO.Merchant merchant = await dataAccess.GetMerchantByIdAsync(new DTO.MerchantId() { UUID = Guid.NewGuid().ToString() }).ConfigureAwait(false);

            Assert.Null(merchant);
        }




    }
}
