using System.Text.Json;
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

        public async Task Invoke(HttpContext httpContext)
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>();
            
            if (exception != null)
            {
                string errorMessage = exception.Error?.Message;
                httpContext.Response.ContentLength = errorMessage.Length;
                httpContext.Response.StatusCode = (int)exception.Error.ToHttpStatus();
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(errorMessage);
            }
        }
    }
}