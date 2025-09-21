using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int AddressID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string ShippingAddress { get; set; } = string.Empty;

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("AddressID")]
        public virtual Address? Address { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
