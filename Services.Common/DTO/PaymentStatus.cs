namespace Services.Common.DTO
{
    /// <summary>
    /// Enumeration of payment statuses.
    /// </summary>
    public enum PaymentStatus
    {
        Submitted,
        Processing,
        Approved,
        Declined,
        Failed
    }
}