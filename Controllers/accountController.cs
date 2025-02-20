using CookieAuthentication_CoreWebAPI.Interface;
using CookieAuthentication_CoreWebAPI.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthentication_CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private readonly IUserService _userService;
        public accountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.IsValidUser(model);
                if (user != null)
                {
                    var claim = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Rolename),
                        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    var userInfo = new
                    {
                        UserID = user.UserID,
                        Username = user.Username,
                        RoleName = user.Rolename,
                        Message = "Login successful.",
                        IsSuccessfullLogin = true
                    };
                    return Ok(userInfo);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid credentials." });
                }
            }
            return BadRequest(new { Message = "Invalid username and password." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Logout successful." });
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            var respone = new
            {
                Message = "Access denied, you do not have permission to access this resource",
                IsSuccessfullLogin = false
            };
            return StatusCode(StatusCodes.Status403Forbidden, respone);
        }
    }
}