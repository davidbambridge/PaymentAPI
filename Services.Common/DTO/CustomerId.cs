namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a customer Id
    /// </summary>
    public class CustomerId : UniqueItem
    {
        /// <summary>
        /// Initializes a new instance of the Customer Id class.
        /// </summary>
        public CustomerId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Customer Id class.
        /// </summary>
        /// <param name="other">A customer id</param>
        public CustomerId(CustomerId other) : base(other) { }
    }
}
