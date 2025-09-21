using EcommerceAPI.Models;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    Task<Order?> GetOrderWithDetailsAsync(int orderId);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
    // Task<Order> GetByIdAsync(object orderId);
}
