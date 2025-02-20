using System.ComponentModel.DataAnnotations;

namespace CookieAuthentication_CoreWebAPI.Model
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
