using EcommerceAPI.Models;

public interface IOrderItemRepository
{
    Task<OrderItem> AddAsync(OrderItem orderItem);
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(int id);
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(OrderItem orderItem);
}
