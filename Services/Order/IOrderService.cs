using EcommerceAPI.dto;
using EcommerceAPI.Models;
using EcommerceAPI.Utils.Helpers; // Added missing using statement for ApiResponse

namespace EcommerceAPI.Services.Order
{
    public interface IOrderService
    {
        Task<ApiResponse<Models.Order>> CreateOrderAsync(CreateOrderDto createOrderDto); // Fully qualified Order type to avoid namespace conflict
        Task<ApiResponse<IEnumerable<Models.Order>>> GetOrdersByCustomerAsync(int customerId); // Fully qualified Order type
        Task<ApiResponse<Models.Order>> GetOrderByIdAsync(int orderId); // Fully qualified Order type
        Task<ApiResponse<Models.Order>> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateStatusDto); // Fully qualified Order type
        Task<ApiResponse<bool>> CancelOrderAsync(int orderId);
        Task<ApiResponse<IEnumerable<Models.Order>>> GetAllOrdersAsync(); // Fully qualified Order type
    }
}
