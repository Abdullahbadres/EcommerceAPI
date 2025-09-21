using EcommerceAPI.dto;
using EcommerceAPI.Models;
using EcommerceAPI.Utils;
using EcommerceAPI.Utils.Helpers;

namespace EcommerceAPI.Services.Payment
{
    public interface IPaymentServiceWithCustomReturnType
    {
        Task<ApiResponse<Models.Payment>> ProcessPaymentAsync(ProcessPaymentDto processPaymentDto);
        Task<ApiResponse<Models.Payment>> GetPaymentByIdAsync(int paymentId);
        Task<ApiResponse<IEnumerable<Models.Payment>>> GetPaymentsByOrderIdAsync(int orderId);
        Task<ApiResponse<Models.Payment>> RefundPaymentAsync(int paymentId);
        Task<ApiResponse<bool>> VerifyPaymentAsync(string transactionId);
    }
}
