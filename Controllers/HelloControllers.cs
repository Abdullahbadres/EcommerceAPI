//routing controller, patternnya dari sini dulu pertama

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult GetWelcomeMsg()
    {
        // Return a simple welcome message
        return Ok("Hello, World!");
    }

    //tambah status bad request
    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request example.");
    }


    [HttpGet("status")]
    public IActionResult GetStatusCodeExample()
    {
        // Return a 201 Created status code
        return Ok(new
        {
            Status = "Healthy",
            Service = "Ecommerce API",
            Uptime = DateTime.Now
        });
    }
}