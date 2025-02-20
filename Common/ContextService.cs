namespace CookieAuthentication_CoreWebAPI.Common
{
    public class ContextService
    {
        public string servicesdbConn { get; set; }
        public string GetConnectionString()
        {
            return servicesdbConn;
        }
    }
}
