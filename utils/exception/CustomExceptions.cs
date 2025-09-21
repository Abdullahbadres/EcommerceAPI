using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Utils.Exceptions
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }
        public object? Details { get; }

        public BusinessException(string message, string errorCode = "BUSINESS_ERROR", object? details = null) 
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }

    public class ResourceNotFoundException : Exception
    {
        public string ResourceType { get; }
        public object ResourceId { get; }

        public ResourceNotFoundException(string resourceType, object resourceId) 
            : base($"{resourceType} with ID '{resourceId}' was not found.")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }

    public class DuplicateResourceException : Exception
    {
        public string ResourceType { get; }
        public string Field { get; }
        public object Value { get; }

        public DuplicateResourceException(string resourceType, string field, object value) 
            : base($"{resourceType} with {field} '{value}' already exists.")
        {
            ResourceType = resourceType;
            Field = field;
            Value = value;
        }
    }

    public class InsufficientStockException : BusinessException
    {
        public int ProductId { get; }
        public int RequestedQuantity { get; }
        public int AvailableStock { get; }

        public InsufficientStockException(int productId, int requestedQuantity, int availableStock) 
            : base($"Insufficient stock for product {productId}. Requested: {requestedQuantity}, Available: {availableStock}", 
                   "INSUFFICIENT_STOCK")
        {
            ProductId = productId;
            RequestedQuantity = requestedQuantity;
            AvailableStock = availableStock;
        }
    }

    public class PaymentFailedException : BusinessException
    {
        public string PaymentMethod { get; }
        public decimal Amount { get; }
        public string Reason { get; }

        public PaymentFailedException(string paymentMethod, decimal amount, string reason) 
            : base($"Payment failed: {reason}", "PAYMENT_FAILED")
        {
            PaymentMethod = paymentMethod;
            Amount = amount;
            Reason = reason;
        }
    }

    public class InvalidOperationException : BusinessException
    {
        public InvalidOperationException(string message) 
            : base(message, "INVALID_OPERATION")
        {
        }
    }
}
