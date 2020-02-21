namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a Card Id
    /// </summary>
    public class CardId : UniqueItem
    {
        /// <summary>
        /// Initializes a new instance of the Card Id class.
        /// </summary>
        public CardId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Card Id class.
        /// </summary>
        /// <param name="other">A Card Id</param>
        public CardId(CardId other) : base(other) { }
    }
}