using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Table("shopping_carts")]
    public class ShoppingCart
    {
        [Key]
        public int CartID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products? Product { get; set; }
    }
}
