using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(100)]
        public string TransactionID { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedDate { get; set; }

        // Navigation property
        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
    }
}
