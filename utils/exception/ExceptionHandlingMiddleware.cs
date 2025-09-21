using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using EcommerceAPI.Utils.Exceptions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Middleware // Added missing namespace declaration
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred. TraceId: {TraceId}", context.TraceIdentifier);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Success = false,
                Message = "An error occurred processing your request",
                TraceId = context.TraceIdentifier,
                Timestamp = DateTime.UtcNow
            };

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = 400;
                    response.Message = validationEx.Message;
                    response.ErrorCode = "VALIDATION_ERROR";
                    break;

                case BusinessException businessEx:
                    context.Response.StatusCode = 400;
                    response.Message = businessEx.Message;
                    response.ErrorCode = businessEx.ErrorCode;
                    response.Details = businessEx.Details;
                    break;

                case ResourceNotFoundException notFoundEx:
                    context.Response.StatusCode = 404;
                    response.Message = notFoundEx.Message;
                    response.ErrorCode = "RESOURCE_NOT_FOUND";
                    response.Details = new { ResourceType = notFoundEx.ResourceType, ResourceId = notFoundEx.ResourceId };
                    break;

                case DuplicateResourceException duplicateEx:
                    context.Response.StatusCode = 409;
                    response.Message = duplicateEx.Message;
                    response.ErrorCode = "DUPLICATE_RESOURCE";
                    response.Details = new { 
                        ResourceType = duplicateEx.ResourceType, 
                        Field = duplicateEx.Field, 
                        Value = duplicateEx.Value 
                    };
                    break;

                // case InsufficientStockException stockEx:
                //     context.Response.StatusCode = 400;
                //     response.Message = stockEx.Message;
                //     response.ErrorCode = stockEx.ErrorCode;
                //     response.Details = new { 
                //         ProductId = stockEx.ProductId, 
                //         RequestedQuantity = stockEx.RequestedQuantity, 
                //         AvailableStock = stockEx.AvailableStock 
                //     };
                //     break;

                // case PaymentFailedException paymentEx:
                //     context.Response.StatusCode = 402;
                //     response.Message = paymentEx.Message;
                //     response.ErrorCode = paymentEx.ErrorCode;
                //     response.Details = new { 
                //         PaymentMethod = paymentEx.PaymentMethod, 
                //         Amount = paymentEx.Amount, 
                //         Reason = paymentEx.Reason 
                //     };
                //     break;

                case KeyNotFoundException keyNotFoundEx:
                    context.Response.StatusCode = 404;
                    response.Message = keyNotFoundEx.Message;
                    response.ErrorCode = "KEY_NOT_FOUND";
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = 401;
                    response.Message = "Unauthorized access";
                    response.ErrorCode = "UNAUTHORIZED";
                    break;

                case ArgumentException argEx:
                    context.Response.StatusCode = 400;
                    response.Message = argEx.Message;
                    response.ErrorCode = "INVALID_ARGUMENT";
                    break;

                case System.InvalidOperationException invalidOpEx when invalidOpEx.Message.Contains("sequence"):
                    context.Response.StatusCode = 404;
                    response.Message = "Resource not found";
                    response.ErrorCode = "RESOURCE_NOT_FOUND";
                    break;

                case TimeoutException:
                    context.Response.StatusCode = 408;
                    response.Message = "Request timeout";
                    response.ErrorCode = "REQUEST_TIMEOUT";
                    break;

                default:
                    context.Response.StatusCode = 500;
                    response.Message = "An internal server error occurred";
                    response.ErrorCode = "INTERNAL_SERVER_ERROR";
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string TraceId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public object? Details { get; set; }
    }
}
