using EcommerceAPI.dto;
using EcommerceAPI.Models;
// using EcommerceAPI.Repositories.User;
using EcommerceAPI.Services.Validation;
using EcommerceAPI.Utils.Exceptions;
using EcommerceAPI.Utils.Helpers;

namespace EcommerceAPI.Services.Payment
{
    public class PaymentService : IPaymentServiceWithCustomReturnType
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;

        public PaymentService(IUnitOfWork unitOfWork, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

        public async Task<ApiResponse<Models.Payment>> ProcessPaymentAsync(ProcessPaymentDto processPaymentDto)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(processPaymentDto.OrderId);
            if (order == null)
            {
                throw new ResourceNotFoundException("Order", processPaymentDto.OrderId);
            }

            await _validationService.ValidatePaymentAmountAsync(order.TotalAmount, processPaymentDto.Amount);

            // Simulate payment processing
            var isPaymentSuccessful = SimulatePaymentProcessing(processPaymentDto);

            if (!isPaymentSuccessful)
            {
                throw new PaymentFailedException(
                    processPaymentDto.PaymentMethod,
                    processPaymentDto.Amount,
                    "Payment processing failed"
                );
            }

            var payment = new Models.Payment
            {
                OrderID = processPaymentDto.OrderId,
                Amount = processPaymentDto.Amount,
                PaymentMethod = processPaymentDto.PaymentMethod,
                TransactionID = GenerateTransactionId(),
                PaymentDate = DateTime.UtcNow,
                Status = "Completed"
            };

            await _unitOfWork.Payments.AddAsync(payment);

            // Update order status
            order.Status = "Paid";
            await _unitOfWork.Orders.UpdateAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<Models.Payment>
            {
                Success = true,
                Message = "Payment processed successfully",
                Data = payment
            };
        }

        public async Task<ApiResponse<Models.Payment>> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new ResourceNotFoundException("Payment", paymentId);
            }

            return new ApiResponse<Models.Payment>
            {
                Success = true,
                Message = "Payment retrieved successfully",
                Data = payment
            };
        }

        public async Task<ApiResponse<IEnumerable<Models.Payment>>> GetPaymentsByOrderIdAsync(int orderId)
        {
            var payments = await _unitOfWork.Payments.GetByOrderIdAsync(orderId);

            return new ApiResponse<IEnumerable<Models.Payment>>
            {
                Success = true,
                Message = "Payments retrieved successfully",
                Data = payments
            };
        }

        public async Task<ApiResponse<Models.Payment>> RefundPaymentAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new ResourceNotFoundException("Payment", paymentId);
            }

            if (payment.Status == "Refunded")
            {
                throw new BusinessException("Payment has already been refunded", "ALREADY_REFUNDED");
            }

            payment.Status = "Refunded";
            await _unitOfWork.Payments.UpdateAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<Models.Payment>
            {
                Success = true,
                Message = "Payment refunded successfully",
                Data = payment
            };
        }

        public async Task<ApiResponse<bool>> VerifyPaymentAsync(string transactionId)
        {
            var payment = await _unitOfWork.Payments.GetByTransactionIdAsync(transactionId);
            
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Payment verification completed",
                Data = payment != null && payment.Status == "Completed"
            };
        }

        private bool SimulatePaymentProcessing(ProcessPaymentDto processPaymentDto)
        {
            // Simulate payment processing logic
            // In real implementation, this would integrate with payment gateways
            return processPaymentDto.Amount > 0 && !string.IsNullOrEmpty(processPaymentDto.PaymentMethod);
        }

        private string GenerateTransactionId()
        {
            return $"TXN_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}
