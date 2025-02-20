using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthentication_CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController() { }

        [HttpGet, Route("GetEmployee")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(new { Message = "This is protected method, you have to have cookie Authentication" });
        }

        [HttpGet, Route("GetPartTimeEmployee")]
        public IActionResult GetPartTimeEmployee()
        {
            return Ok(new
            {
                Message = "This is not protected method, you can get it without cookie authenticate",
            });
        }
    }
}
