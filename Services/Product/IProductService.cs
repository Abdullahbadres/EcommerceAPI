using EcommerceAPI.Models;
using EcommerceAPI.dto;
using EcommerceAPI.Utils;

namespace EcommerceAPI.Services.Product
{
    public interface IProductService
    {
        Task<ProductResponseDTO> CreateAsync(CreateProductDto dto);
        Task<IEnumerable<ProductResponseDTO>> GetAllProduct();
        Task<ProductResponseDTO?> GetByIdAsync(int id);
        Task<PagedResponse<ProductResponseDTO>> GetProductsAsync(FilterDto filter);
        Task<Products?> UpdateAsync(int id, ProductUpdatedDto dto);
        Task<Products?> PatchAsync(int id, ProductUpdatedDto dto);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> HarddeleteAsync(int id);
    }
}
