namespace Services.Common.DTO
{
    /// <summary>
    /// Represents an Address Id
    /// </summary>
    public class AddressId : UniqueItem
    {
        /// <summary>
        /// Initializes a new instance of the Address Id class.
        /// </summary>
        public AddressId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Address Id class.
        /// </summary>
        /// <param name="other">An address id</param>
        public AddressId(AddressId other) : base(other) { }
    }
}
