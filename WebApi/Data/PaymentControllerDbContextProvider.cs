using Microsoft.Extensions.Configuration;

using WebApi.Interfaces;

namespace WebApi.Data
{
    /// <summary>
    /// Provider class for db context
    /// </summary>
    public class PaymentControllerDbContextProvider : IPaymentControllerDbContext
    {
        private string _connectionString { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentControllerDbContextProvider(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        /// <summary>
        /// Create a new instance of the db context
        /// </summary>
        /// <returns></returns>
        public PaymentControllerDbContext Create()
        {
            return new PaymentControllerDbContext(_connectionString);
        }
    }
}
