using EcommerceAPI.dto;
using EcommerceAPI.Models;
// using EcommerceAPI.Repositories;
using EcommerceAPI.Services.Validation;
using EcommerceAPI.Utils.Exceptions;
using EcommerceAPI.Utils.Helpers; // Added missing using statement for ApiResponse

namespace EcommerceAPI.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;

        public OrderService(IUnitOfWork unitOfWork, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

public async Task<ApiResponse<Models.Order>> CreateOrderAsync(CreateOrderDto createOrderDto)
{
    await _validationService.ValidateUserExistsAsync(createOrderDto.CustomerID);

    var cartItems = await _unitOfWork.Carts.GetCartItemsByCustomerIdAsync(createOrderDto.CustomerID);
    if (!cartItems.Any())
    {
        throw new BusinessException("Cannot create order with empty cart", "EMPTY_CART");
    }

            var order = new Models.Order // Fully qualified Order type
            {
                CustomerID = createOrderDto.CustomerID, // Fixed property name
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = cartItems.Sum(item => (item.Product?.Price ?? 0) * item.Quantity), // Added null check for Product
                ShippingAddress = createOrderDto.ShippingAddress
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // Create order items
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product?.Price ?? 0, // Added null check for Product
                    TotalPrice = (cartItem.Product?.Price ?? 0) * cartItem.Quantity // Calculate TotalPrice
                };

                await _unitOfWork.OrderItems.AddAsync(orderItem);
            }

            // Clear cart after creating order
            await _unitOfWork.Carts.ClearCartAsync(createOrderDto.CustomerID); // Fixed property name
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<Models.Order>.SuccessResponse("Order created successfully", order); // Use ApiResponse helper method
        }

        public async Task<ApiResponse<IEnumerable<EcommerceAPI.Models.Order>>> GetOrdersByCustomerAsync(int customerId) // Fully qualified Order type
        {
            await _validationService.ValidateUserExistsAsync(customerId);

            var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(customerId);

            return ApiResponse<IEnumerable<EcommerceAPI.Models.Order>>.SuccessResponse("Orders retrieved successfully", orders); // Use ApiResponse helper method"; // Use ApiResponse helper method
        }

        public async Task<ApiResponse<Models.Order>> GetOrderByIdAsync(int orderId) // Fully qualified Order type
        {
            var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(orderId);
            if (order == null)
            {
                throw new ResourceNotFoundException("Order", orderId);
            }

            return ApiResponse<Models.Order>.SuccessResponse("Order retrieved successfully", order); // Use ApiResponse helper method
        }

        public async Task<ApiResponse<Models.Order>> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateStatusDto) // Fully qualified Order type
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ResourceNotFoundException("Order", orderId);
            }

            order.Status = updateStatusDto.Status;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<Models.Order>.SuccessResponse("Order status updated successfully", order); // Use ApiResponse helper method
        }

        public async Task<ApiResponse<bool>> CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ResourceNotFoundException("Order", orderId);
            }

            if (order.Status == "Shipped" || order.Status == "Delivered")
            {
                throw new BusinessException("Cannot cancel order that has been shipped or delivered", "CANNOT_CANCEL_ORDER");
            }

            order.Status = "Cancelled";
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Order cancelled successfully"); // Use ApiResponse helper method
        }

        public async Task<ApiResponse<IEnumerable<Models.Order>>> GetAllOrdersAsync() // Fully qualified Order type
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();

            return ApiResponse<IEnumerable<Models.Order>>.SuccessResponse("All orders retrieved successfully", orders); // Use ApiResponse helper method
        }
    }
}
