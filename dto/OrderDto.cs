using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.dto
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }

    public class OrderResponseDto
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public List<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
    }

    public class OrderItemResponseDto
    {
        public int OrderItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
