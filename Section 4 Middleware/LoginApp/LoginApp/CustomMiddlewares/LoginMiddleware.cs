using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace LoginApp.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method.Equals("POST"))
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();
                Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
                if (queryDict.ContainsKey("email") && queryDict.ContainsKey("password"))
                {
                    string email = queryDict["email"][0];
                    string password = queryDict["password"][0];
                    if (email.Equals("admin@example.com") && password.Equals("admin1234"))
                    {
                        httpContext.Response.StatusCode = 200;
                        await httpContext.Response.WriteAsync("Successful login");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid login");
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    if (!queryDict.ContainsKey("email"))
                    {
                        await httpContext.Response.WriteAsync("Invalid input for 'email'\n");
                    }
                    if (!queryDict.ContainsKey("password"))
                    {
                        await httpContext.Response.WriteAsync("Invalid input for 'password'");
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync("No response");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
