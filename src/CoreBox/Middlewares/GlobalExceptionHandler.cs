using System.Text;
using System.Threading.Tasks;
using CoreBox.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CoreBox.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (exception != null)
            {
                string errorMessage = exception.Error != null ? exception.Error.Message : string.Empty;
                httpContext.Response.ContentLength = errorMessage.Length;
                httpContext.Response.StatusCode = (int)exception.Error.ToHttpStatus();
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(errorMessage, Encoding.UTF8);
            }
        }
    }
}