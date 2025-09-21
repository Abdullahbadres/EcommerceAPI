using EcommerceAPI.Models;

public interface IProductRepository
{
    Task<IEnumerable<Products>> GetAllAsync();
    Task<Products?> GetByIdAsync(int id);
    Task<Products?> GetByNameAsync(string name);
    Task AddAsync(Products product);
    Task UpdateAsync(Products product);
    Task DeleteAsync(Products product);
    Task<IEnumerable<Products>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Products>> SearchAsync(string searchTerm);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Products>> GetActiveProductsAsync();
    Task<IEnumerable<Products>> GetLowStockProductsAsync(int threshold);
}
