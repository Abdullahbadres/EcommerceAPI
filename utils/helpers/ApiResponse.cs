using EcommerceAPI.Models;

namespace EcommerceAPI.Utils.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public object? Errors { get; set; }
        public string TraceId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, object? errors = null, string traceId = "")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                TraceId = traceId,
                Timestamp = DateTime.UtcNow
            };
        }

        internal static ApiResponse<Order> SuccessResponse(string v, Order order)
        {
            throw new NotImplementedException();
        }

        internal static ApiResponse<IEnumerable<Order>> SuccessResponse(string v, IEnumerable<Order> enumerable)
        {
            throw new NotImplementedException();
        }
    }

    public class PaginatedResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<T> Data { get; set; } = new();
        public PaginationInfo Pagination { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
