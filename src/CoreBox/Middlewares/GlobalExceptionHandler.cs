using System.Text;
using System.Threading.Tasks;
using CoreBox.Extensions;
using CoreBox.Types;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CoreBox.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>();

                if (exception != null)
                {
                    string errorMessage = exception.Error != null ? exception.Error.Message : string.Empty;
                    context.Response.ContentLength = errorMessage.Length;
                    context.Response.StatusCode = (int)exception.Error.ToHttpStatus();
                    context.Response.ContentType = MimeType.json;
                    await context.Response.WriteAsync(errorMessage, Encoding.UTF8);
                }
            }
        }
    }
}