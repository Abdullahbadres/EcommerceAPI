using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWelcomeMsg()
        {
            var response = "Hello world";
            return Ok(response);

        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                Status = "Healthy",
                Service = "E-Commerce API",
                Uptime = DateTime.Now
            });
        }
    }
}
