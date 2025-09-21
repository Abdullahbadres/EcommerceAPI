using EcommerceAPI.Models;

public interface ICartRepository
{
    Task<ShoppingCart> AddAsync(ShoppingCart cart);
    Task<IEnumerable<ShoppingCart>> GetAllAsync();
    Task<ShoppingCart?> GetByIdAsync(int id);
    Task<ShoppingCart?> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<ShoppingCart>> GetCartItemsByCustomerIdAsync(int customerId);
    Task UpdateAsync(ShoppingCart cart);
    Task DeleteAsync(ShoppingCart cart);
    Task ClearCartAsync(int customerId);
}
