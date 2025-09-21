using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products? Product { get; set; }
    }
}
