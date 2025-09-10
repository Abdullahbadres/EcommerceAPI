using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}