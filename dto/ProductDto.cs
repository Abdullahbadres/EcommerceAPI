using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EcommerceAPI.Utils.Validation;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 150 characters")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [PriceRange(0.01, 1000000)] // Added custom price range validation
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stock quantity is required")]
    [StockQuantity] // Added custom stock validation
    public int Stock { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
    public int CategoryID { get; set; } // Fixed typo from CtegoryID

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Url(ErrorMessage = "Please provide a valid image URL")]
    public string ImageUrl { get; set; } = string.Empty;
}

public class ProductResponseDTO
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

public class ProductUpdatedDto // Fixed typo from ProducUpdatetDto
{
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 150 characters")]
    public string? ProductName { get; set; }

    [PriceRange(0.01, 1000000)]
    public decimal? Price { get; set; }

    [StockQuantity]
    public int? Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
    public int? CategoryID { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "Please provide a valid image URL")]
    public string? ImageUrl { get; set; }
}

public class ProductSearchDto
{
    [StringLength(100, ErrorMessage = "Search term cannot exceed 100 characters")]
    public string? SearchTerm { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
    public int? CategoryId { get; set; }

    [PriceRange(0, 1000000)]
    public decimal? MinPrice { get; set; }

    [PriceRange(0, 1000000)]
    public decimal? MaxPrice { get; set; }

    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be positive")]
    public int PageNumber { get; set; } = 1;

    [RegularExpression(@"^(name|price|stock|category)$", ErrorMessage = "Sort by must be: name, price, stock, or category")]
    public string SortBy { get; set; } = "name";

    [RegularExpression(@"^(asc|desc)$", ErrorMessage = "Sort order must be 'asc' or 'desc'")]
    public string SortOrder { get; set; } = "asc";
}
