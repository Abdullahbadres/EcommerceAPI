using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDBContext _db;

    public CategoryRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Category> AddAsync(Category category)
    {
        await _db.Categories.AddAsync(category);
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.FindAsync(id);
    }

    public async Task UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }
}
