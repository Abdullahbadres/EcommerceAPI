using System.ComponentModel.DataAnnotations;//kedua

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;
    [StringLength(500)]
    //relasi property ke Product
    public string Description { get; set; } = string.Empty;

    //navigasi properti ke Product = one to many relationship
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}