using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Services.Common.Model;


namespace WebApi.Data
{
    /// <summary>
    /// DbContext class
    /// </summary>
    public class PaymentControllerDbContext : DbContext
    {
        private readonly string _connectionString;


        public PaymentControllerDbContext() { }

        /// <summary>
        /// Creates a new instance of the dbContext class
        /// </summary>
        /// <param name="configuration">Configuration values</param>
        public PaymentControllerDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Customers table
        /// </summary>
        public virtual DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// payments table
        /// </summary>
        public virtual DbSet<PaymentItem> Payments { get; set; }

        /// <summary>
        /// merchants table
        /// </summary>
        public virtual DbSet<Merchant> Merchant { get; set; }

        /// <summary>
        /// Cards table
        /// </summary>
        public virtual DbSet<Card> Card { get; set; }

        /// <summary>
        /// Address table
        /// </summary>
        public virtual DbSet<Address> Address { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
