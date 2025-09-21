using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EcommerceAPI.Utils.Helpers;

namespace EcommerceAPI.Utils.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = ApiResponse<object>.ErrorResponse(
                    "Validation failed",
                    errors,
                    context.HttpContext.TraceIdentifier
                );

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
