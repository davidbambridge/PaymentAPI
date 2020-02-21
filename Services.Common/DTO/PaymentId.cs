namespace Services.Common.DTO
{
    /// <summary>
    /// Represents a payment Id
    /// </summary>
    public class PaymentId : UniqueItem
    {
        /// <summary>
        /// Initializes a new instance of the payment Id class.
        /// </summary>
        public PaymentId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the payment Id class.
        /// </summary>
        /// <param name="other">A Payment Id</param>
        public PaymentId(PaymentId other) : base(other)
        {
        }
    }
}