using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace customerDemoWebApi.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BasicAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string apiUsername = "ApiUsername";
        private const string apiPassword = "ApiPassword";


        public BasicAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var authHeader = httpContext.Request.Headers.Authorization.ToString();

            if(authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUnamePassword = authHeader.Substring("Basic".Length).Trim();
                Encoding encoding = Encoding.UTF8;
                string UnamePassword = encoding.GetString(Convert.FromBase64String(encodedUnamePassword));
                int index = UnamePassword.IndexOf(":");
                var username = UnamePassword.Substring(0, index);
                var password = UnamePassword.Substring (index + 1);

                var appSettings = httpContext.RequestServices.GetRequiredService<IConfiguration>();
                var usernameConfig = appSettings.GetValue<string>(apiUsername);
                var passwordConfig = appSettings.GetValue<string>(apiPassword);
                
                if(username.Equals(usernameConfig) && password.Equals(passwordConfig))
                {
                    await _next.Invoke(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = 401;

                    await httpContext.Response.WriteAsync("Invalid Authorization is passed");

                }




            }
            else
            {
                httpContext.Response.StatusCode = 401;

                await httpContext.Response.WriteAsync("Authorization is not given");

            }

            return;
        }
    }

    
}
