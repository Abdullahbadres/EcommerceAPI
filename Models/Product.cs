using System.ComponentModel.DataAnnotations;//ketiga
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string ProductName { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    //relasi property ke Category
    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }
    
}