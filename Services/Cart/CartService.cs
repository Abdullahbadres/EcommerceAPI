using EcommerceAPI.dto;
using EcommerceAPI.Models;
// using EcommerceAPI.Repositories.User;
using EcommerceAPI.Services.Validation;
using EcommerceAPI.Utils.Exceptions;
using EcommerceAPI.Utils;
using EcommerceAPI.Utils.Helpers;

namespace EcommerceAPI.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;

        public CartService(IUnitOfWork unitOfWork, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

        public async Task<ApiResponse<ShoppingCart>> AddToCartAsync(AddToCartDto addToCartDto)
        {
            await _validationService.ValidateUserExistsAsync(addToCartDto.CustomerID);
            await _validationService.ValidateProductExistsAsync(addToCartDto.ProductID);
            await _validationService.ValidateProductStockAsync(addToCartDto.ProductID, addToCartDto.Quantity);

            var existingCartItems = await _unitOfWork.Carts.GetCartItemsByCustomerIdAsync(addToCartDto.CustomerID);
            var existingCartItem = existingCartItems.FirstOrDefault(c => c.ProductID == addToCartDto.ProductID);
            
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addToCartDto.Quantity;
                existingCartItem.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Carts.UpdateAsync(existingCartItem);
            }
            else
            {
                var cartItem = new ShoppingCart
                {
                    CustomerID = addToCartDto.CustomerID,
                    ProductID = addToCartDto.ProductID,
                    Quantity = addToCartDto.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Carts.AddAsync(cartItem);
                existingCartItem = cartItem;
            }

            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<ShoppingCart>
            {
                Success = true,
                Message = "Item added to cart successfully",
                Data = existingCartItem
            };
        }

        public async Task<ApiResponse<IEnumerable<ShoppingCart>>> GetCartItemsAsync(int customerId)
        {
            await _validationService.ValidateUserExistsAsync(customerId);

            var cartItems = await _unitOfWork.Carts.GetCartItemsByCustomerIdAsync(customerId);

            return new ApiResponse<IEnumerable<ShoppingCart>>
            {
                Success = true,
                Message = "Cart items retrieved successfully",
                Data = cartItems
            };
        }

        public async Task<ApiResponse<ShoppingCart>> UpdateCartItemAsync(int cartId, UpdateCartItemDto updateCartDto)
        {
            var cartItem = await _unitOfWork.Carts.GetByIdAsync(cartId);
            if (cartItem == null)
            {
                throw new ResourceNotFoundException("Cart item", cartId);
            }

            await _validationService.ValidateProductStockAsync(cartItem.ProductID, updateCartDto.Quantity);

            cartItem.Quantity = updateCartDto.Quantity;
            cartItem.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Carts.UpdateAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<ShoppingCart>
            {
                Success = true,
                Message = "Cart item updated successfully",
                Data = cartItem
            };
        }

        public async Task<ApiResponse<bool>> RemoveFromCartAsync(int cartId)
        {
            var cartItem = await _unitOfWork.Carts.GetByIdAsync(cartId);
            if (cartItem == null)
            {
                throw new ResourceNotFoundException("Cart item", cartId);
            }

            await _unitOfWork.Carts.DeleteAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Item removed from cart successfully",
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> ClearCartAsync(int customerId)
        {
            await _validationService.ValidateUserExistsAsync(customerId);

            await _unitOfWork.Carts.ClearCartAsync(customerId);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Cart cleared successfully",
                Data = true
            };
        }

        public async Task<ApiResponse<decimal>> GetCartTotalAsync(int customerId)
        {
            await _validationService.ValidateUserExistsAsync(customerId);

            var cartItems = await _unitOfWork.Carts.GetCartItemsByCustomerIdAsync(customerId);
            var total = cartItems.Sum(item => (item.Product?.Price ?? 0) * item.Quantity);

            return new ApiResponse<decimal>
            {
                Success = true,
                Message = "Cart total calculated successfully",
                Data = total
            };
        }
    }
}
