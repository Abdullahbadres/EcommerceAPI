using EcommerceAPI.dto;
using EcommerceAPI.Models;
using EcommerceAPI.Utils.Helpers; // Added missing using statement for ApiResponse

namespace EcommerceAPI.Services.Cart
{
    public interface ICartService
    {
        Task<ApiResponse<ShoppingCart>> AddToCartAsync(AddToCartDto addToCartDto);
        Task<ApiResponse<IEnumerable<ShoppingCart>>> GetCartItemsAsync(int customerId);
        Task<ApiResponse<ShoppingCart>> UpdateCartItemAsync(int cartId, UpdateCartItemDto updateCartDto);
        Task<ApiResponse<bool>> RemoveFromCartAsync(int cartId);
        Task<ApiResponse<bool>> ClearCartAsync(int customerId);
        Task<ApiResponse<decimal>> GetCartTotalAsync(int customerId);
    }
}
