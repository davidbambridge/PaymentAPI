using System.ComponentModel.DataAnnotations;

namespace Services.Common.Model
{
    /// <summary>
    /// Represents a merchant
    /// </summary>
    public class Merchant
    {
        /// <summary>
        /// Gets or sets the id of the merchant
        /// </summary>
        [Key]
        public string MerchantId { get; set; }
    }
}
