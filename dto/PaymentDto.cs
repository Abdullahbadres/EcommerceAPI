using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.dto
{
    public class ProcessPaymentDto
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(100)]
        public string TransactionID { get; set; } = string.Empty;
        public int OrderId { get; set; }
    }

    public class PaymentResponseDto
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TransactionID { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}
