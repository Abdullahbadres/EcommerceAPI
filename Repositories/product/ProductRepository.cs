using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using EcommerceAPI.Configurations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _db;
    public ProductRepository(ApplicationDBContext db) => _db = db;

    public async Task<IEnumerable<Products>> GetAllAsync()
        => await _db.Products.Include(p => p.Category).ToListAsync();

    public async Task<Products?> GetByIdAsync(int id)
        => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == id);

    public async Task<Products?> GetByNameAsync(string name)
        => await _db.Products.FirstOrDefaultAsync(p => p.ProductName == name);

    public async Task AddAsync(Products product)
        => await _db.Products.AddAsync(product);

    public Task UpdateAsync(Products product)
    {
        _db.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Products product)
    {
        _db.Products.Remove(product);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Products>> GetByCategoryAsync(int categoryId)
        => await _db.Products.Where(p => p.CategoryID == categoryId).ToListAsync();

    public async Task<IEnumerable<Products>> SearchAsync(string searchTerm)
        => await _db.Products
            .Where(p => p.ProductName.Contains(searchTerm))
            .Include(p => p.Category)
            .ToListAsync();

    public async Task<bool> ExistsAsync(int id)
        => await _db.Products.AnyAsync(p => p.ProductID == id);

    public async Task<IEnumerable<Products>> GetActiveProductsAsync()
        => await _db.Products
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .ToListAsync();

    public async Task<IEnumerable<Products>> GetLowStockProductsAsync(int threshold)
        => await _db.Products
            .Where(p => p.Stock <= threshold)
            .Include(p => p.Category)
            .ToListAsync();
}
