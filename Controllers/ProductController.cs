using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;

    }

    //reccommend approach strong typing
    // public async Task<ActionResult<Product>> GetProductsById(int id)
    // {
    //     await Task.Delay(100); // Simulate async operation
    //     var NewProduct = new Product
    //     {
    //         ProductName = "Sample Product",
    //         Price = 100,
    //         Stock = 10,
    //         CategoryId = 1
    //     };
    //     if (NewProduct == null)
    //     {
    //         return NotFound();//http status 404
    //     }
    //     return NewProduct; //http status 200
    // }

    // //when returning  deffrence type
    // public async Task<IActionResult> GetProductById(int id)
    // {
    //     await Task.Delay(100);
    //     var NewProduct = new Products
    //     {
    //         ProductName = "laptop",
    //         CategoryID = 1,
    //         Price = 1000,
    //         Stock = 5
    //     };
    //     if (NewProduct == null)
    //     {
    //         return NotFound(); //http 404

    //     }
    //     return Ok(NewProduct); // explicit OK

    // }

    [HttpGet] //ini anotasi untuk method get
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts([FromQuery] FilterDto filterDto)
    {
        //url : GET /api.product?name=abc&minPrice=1000&sortBy=price_desc&pageNumber=2
        //otomatis di bind ke FilterDto object
        await Task.Delay(100); // Simulate async operation
        //var products = "this is product";
        return Ok(filterDto);
    }

    [HttpGet("{id}")] //ini anotasi untuk method get by id, id diambil dari url
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        await Task.Delay(100); // Simulate async operation
        var result = $"get product by id {id}";
        _logger.LogInformation("Fetching product with ID: {ProductId}", id);//contoh log info
        _logger.LogWarning("This is a warning message for product ID: {ProductId}", id);//contoh log warning
        _logger.LogError("This is an error message for product ID: {ProductId}", id);//contoh log error
        _logger.LogDebug("Debugging product retrieval for ID: {ProductId}", id);//contoh log debug
        _logger.LogCritical("Critical issue encountered for product ID: {ProductId}", id);//contoh log critical
        _logger.LogTrace("Trace log for product ID: {ProductId}", id);//contoh log trace
        return StatusCode(200, result);
    }

    [HttpGet("search")] //query parameter 
    public async Task<ActionResult<Product>> SearchProducts(string search)
    {
        await Task.Delay(100); // Simulate async operation
        var result = $"search product by {search}";
        return StatusCode(200, result);
    }
        [HttpGet("filter")] // query parameter GET api/product/search?name=laptop
    public async Task<ActionResult<Product>> SearchProduct(string cari)
    {
        await Task.Delay(100);
        var result = $"search product by name = {cari}";
        return StatusCode(200, result);

    }



    [HttpPost] //ini anotasi untuk method post
    public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
    {
        await Task.Delay(100); // Simulate async operation atau call a service
        var NewProduct = new Product
        {
            ProductName = productDto.Name,
            Price = productDto.Price,
            Stock = productDto.Stock,
            CategoryId = productDto.CategoryId
        };
        return StatusCode(201, new BaseResponseDto<Product>//201 itu artinya created
        {
            Success = true,
            Message = "Product created successfully",
            Data = NewProduct
        });
         
    }
}