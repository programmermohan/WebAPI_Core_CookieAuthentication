using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookieAuthentication_CoreWebAPI.Common
{
    public static class ExtensionMethod
    {
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/account/Login";
                    options.LogoutPath = "/api/account/Logout";
                    options.AccessDeniedPath = "/api/account/accessdenied";
                    options.Cookie.Name = "CookieAuthentication";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                });
            return services;
        }
    }
}
