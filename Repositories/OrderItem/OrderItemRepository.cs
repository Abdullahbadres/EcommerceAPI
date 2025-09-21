using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDBContext _db;

    public OrderItemRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<OrderItem> AddAsync(OrderItem orderItem)
    {
        await _db.OrderItems.AddAsync(orderItem);
        return orderItem;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _db.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _db.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.OrderItemID == id);
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
    {
        return await _db.OrderItems
            .Include(oi => oi.Product)
            .Where(oi => oi.OrderID == orderId)
            .ToListAsync();
    }

    public Task UpdateAsync(OrderItem orderItem)
    {
        _db.OrderItems.Update(orderItem);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(OrderItem orderItem)
    {
        _db.OrderItems.Remove(orderItem);
        return Task.CompletedTask;
    }
}
