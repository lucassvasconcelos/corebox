using System.Text;
using System.Text.Json;
using CoreBox.Extensions;
using CoreBox.Types;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CoreBox.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception != null)
        {
            string errorMessage = JsonSerializer.Serialize(new { Error = exception.GetMessage() });
            context.Response.ContentLength = errorMessage.Length;
            context.Response.StatusCode = (int)exception.ToHttpStatus();
            context.Response.ContentType = MimeType.json;
            await context.Response.WriteAsync(errorMessage, Encoding.UTF8, cancellationToken);
        }
        else
        {
            string errorMessage = JsonSerializer.Serialize(new { Error = "Unexpected Error!" });
            context.Response.ContentLength = errorMessage.Length;
            context.Response.StatusCode = 500;
            context.Response.ContentType = MimeType.json;
            await context.Response.WriteAsync(errorMessage, Encoding.UTF8, cancellationToken);
        }

        return true;
    }
}