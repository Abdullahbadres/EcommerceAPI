using EcommerceAPI.Services.Validation;
using EcommerceAPI.Utils.Exceptions;
// using EcommerceAPI.Repositories.User;
using EcommerceAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EcommerceAPI.Services.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidateUserExistsAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ResourceNotFoundException("User", userId);
            }
        }

        public async Task ValidateProductExistsAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ResourceNotFoundException("Product", productId);
            }
        }

        public async Task ValidateProductStockAsync(int productId, int requestedQuantity)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ResourceNotFoundException("Product", productId);
            }

            if (product.Stock < requestedQuantity)
            {
                throw new InsufficientStockException(productId, requestedQuantity, product.Stock);
            }
        }

        public async Task ValidateUniqueUsernameAsync(string username, int? excludeUserId = null)
        {
            var existingUser = await _unitOfWork.Users.GetByUserNameAsync(username);
            if (existingUser != null && (excludeUserId == null || existingUser.UserID != excludeUserId))
            {
                throw new DuplicateResourceException("User", "username", username);
            }
        }

        public async Task ValidateUniqueEmailAsync(string email, int? excludeCustomerId = null)
        {
            await Task.CompletedTask; // Placeholder for future implementation
        }

        public async Task ValidateCategoryExistsAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new ResourceNotFoundException("Category", categoryId);
            }
        }

        public async Task ValidateOrderBelongsToUserAsync(int orderId, int userId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ResourceNotFoundException("Order", orderId);
            }

            if (order.CustomerID != userId)
            {
                throw new UnauthorizedAccessException("You don't have permission to access this order");
            }
        }

        public async Task ValidatePaymentAmountAsync(decimal orderAmount, decimal paymentAmount)
        {
            if (Math.Abs(orderAmount - paymentAmount) > 0.01m) // Allow for small rounding differences
            {
                throw new BusinessException($"Payment amount ({paymentAmount:C}) does not match order total ({orderAmount:C})", "PAYMENT_AMOUNT_MISMATCH");
            }
            await Task.CompletedTask; // Make method properly async
        }

        public void ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidationException("Password is required");
            }

            if (password.Length < 8)
            {
                throw new ValidationException("Password must be at least 8 characters long");
            }

            if (!password.Any(char.IsUpper))
            {
                throw new ValidationException("Password must contain at least one uppercase letter");
            }

            if (!password.Any(char.IsLower))
            {
                throw new ValidationException("Password must contain at least one lowercase letter");
            }

            if (!password.Any(char.IsDigit))
            {
                throw new ValidationException("Password must contain at least one digit");
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new ValidationException("Password must contain at least one special character");
            }
        }

        public void ValidateEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Email is required");
            }

            var emailAttribute = new EmailAddressAttribute();
            if (!emailAttribute.IsValid(email))
            {
                throw new ValidationException("Please provide a valid email address");
            }
        }

        public void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return; // Optional field

            var pattern = @"^(\+62|62|0)8[1-9][0-9]{6,9}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                throw new ValidationException("Please provide a valid Indonesian phone number (e.g., +628123456789, 08123456789)");
            }
        }
    }
}
