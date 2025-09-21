using EcommerceAPI.Utils.Exceptions;

namespace EcommerceAPI.Services.Validation
{
    public interface IValidationService
    {
        Task ValidateUserExistsAsync(int userId);
        Task ValidateProductExistsAsync(int productId);
        Task ValidateProductStockAsync(int productId, int requestedQuantity);
        Task ValidateUniqueUsernameAsync(string username, int? excludeUserId = null);
        Task ValidateUniqueEmailAsync(string email, int? excludeCustomerId = null);
        Task ValidateCategoryExistsAsync(int categoryId);
        Task ValidateOrderBelongsToUserAsync(int orderId, int userId);
        Task ValidatePaymentAmountAsync(decimal orderAmount, decimal paymentAmount);
        void ValidatePasswordStrength(string password);
        void ValidateEmailFormat(string email);
        void ValidatePhoneNumber(string phoneNumber);
    }
}
