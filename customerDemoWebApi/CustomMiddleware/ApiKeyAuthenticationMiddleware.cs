using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace customerDemoWebApi.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string apiKeyPropertyName = "ApiKey";

        public ApiKeyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(!httpContext.Request.Headers.TryGetValue(apiKeyPropertyName, out var extractedApiKey))
            {
                httpContext.Response.StatusCode = 401;

                await httpContext.Response.WriteAsync("ApiKey is not presented");
                return;

            }
            var appSettings = httpContext.RequestServices.GetRequiredService<IConfiguration>();
            var key = appSettings.GetValue<string>(apiKeyPropertyName);

            if (!key.Equals(extractedApiKey))
            {
                httpContext.Response.StatusCode = 401;

                await httpContext.Response.WriteAsync("ApiKey is not valid");
                return;

            }
            await _next(httpContext);


           
        }
    }

}
