using EcommerceAPI.Models;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}
