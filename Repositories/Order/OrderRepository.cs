using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDBContext _db;

    public OrderRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<Order> AddAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderID == id);
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _db.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.CustomerID == customerId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
    {
        return await _db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o => o.Payments)
            .Include(o => o.Shipments)
            .FirstOrDefaultAsync(o => o.OrderID == orderId);
    }

    public Task UpdateAsync(Order order)
    {
        _db.Orders.Update(order);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Order order)
    {
        _db.Orders.Remove(order);
        return Task.CompletedTask;
    }
}
