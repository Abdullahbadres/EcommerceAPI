using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.Utils;
using System.Security.Claims;
using EcommerceAPI.Utils.Helpers;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return userId;
        }

        protected string GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }

        protected bool IsAdmin()
        {
            return GetCurrentUserRole().Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        protected bool IsCustomer()
        {
            return GetCurrentUserRole().Equals("Customer", StringComparison.OrdinalIgnoreCase);
        }

        protected IActionResult Success<T>(T data, string message = "Operation successful")
        {
            return Ok(new Utils.Helpers.ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }

        protected IActionResult Error(string message, object? errors = null)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = errors
            });
        }

        protected IActionResult NotFound(string message)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null
            });
        }

        protected IActionResult Unauthorized(string message = "Unauthorized access")
        {
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null
            });
        }

        protected IActionResult Paginated<T>(List<T> data, int currentPage, int pageSize, int totalItems, string message = "Data retrieved successfully")
        {
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            var response = new PagedResponse<T>
            {
                Success = true,
                Messages = message,
                Data = data,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalItems,
                NextPage = currentPage < totalPages ? currentPage + 1 : null,
                PreviousPage = currentPage > 1 ? currentPage - 1 : null
            };

            return Ok(response);
        }
    }
}
