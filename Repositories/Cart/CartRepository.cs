using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDBContext _db;

    public CartRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<ShoppingCart> AddAsync(ShoppingCart cart)
    {
        await _db.ShoppingCarts.AddAsync(cart);
        return cart;
    }

    public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
    {
        return await _db.ShoppingCarts
            .Include(c => c.Product)
            .Include(c => c.Customer)
            .ToListAsync();
    }

    public async Task<ShoppingCart?> GetByIdAsync(int id)
    {
        return await _db.ShoppingCarts
            .Include(c => c.Product)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(c => c.CartID == id);
    }

    public async Task<ShoppingCart?> GetByCustomerIdAsync(int customerId)
    {
        return await _db.ShoppingCarts
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c => c.CustomerID == customerId);
    }

    public async Task<IEnumerable<ShoppingCart>> GetCartItemsByCustomerIdAsync(int customerId)
    {
        return await _db.ShoppingCarts
            .Include(c => c.Product)
            .Where(c => c.CustomerID == customerId)
            .ToListAsync();
    }

    public async Task UpdateAsync(ShoppingCart cart)
    {
        _db.ShoppingCarts.Update(cart);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(ShoppingCart cart)
    {
        _db.ShoppingCarts.Remove(cart);
        await _db.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int customerId)
    {
        var cartItems = await _db.ShoppingCarts
            .Where(c => c.CustomerID == customerId)
            .ToListAsync();
        
        _db.ShoppingCarts.RemoveRange(cartItems);
    }
}
