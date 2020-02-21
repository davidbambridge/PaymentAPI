namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a Merchant Id
    /// </summary>
    public class MerchantId : UniqueItem
    {
        /// <summary>
        /// Initializes a new instance of the Merchant Id class.
        /// </summary>
        public MerchantId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Merchant Id class.
        /// </summary>
        /// <param name="other">A Merchant Id</param>
        public MerchantId(MerchantId other) : base(other) { }
    }
}