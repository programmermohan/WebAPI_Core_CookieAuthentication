using CookieAuthentication_CoreWebAPI.Model;

namespace CookieAuthentication_CoreWebAPI.Interface
{
    public interface IUserService
    {
        Task<UserModel> IsValidUser(LoginModel loginModel);
    }
}
