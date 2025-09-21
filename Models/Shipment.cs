using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("shipments")]
    public class Shipment
    {
        [Key]
        public int ShipmentID { get; set; }

        [Required]
        public int OrderID { get; set; }

        public Customer Customer { get; set; } = new Customer();

        [Required]
        [StringLength(100)]
        public string TrackingNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Carrier { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Preparing";

        public DateTime ShippedDate { get; set; } = DateTime.UtcNow;

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? ActualDeliveryDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
    }
}
