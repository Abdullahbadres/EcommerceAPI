using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.dto
{
    public class AddToCartDto
    {
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    public class UpdateCartItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    public class CartItemDto
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
