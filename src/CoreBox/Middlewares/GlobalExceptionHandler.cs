using System.Text;
using System.Text.Json;
using CoreBox.Extensions;
using CoreBox.Types;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CoreBox.Middlewares;

public static class GlobalExceptionHandler
{
    public static async Task InvokeAsync(HttpContext context)
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>();

        if (exception != null)
        {
            string errorMessage = JsonSerializer.Serialize(new { Error = exception.Error.GetMessage() });
            context.Response.ContentLength = errorMessage.Length;
            context.Response.StatusCode = (int)exception.Error.ToHttpStatus();
            context.Response.ContentType = MimeType.json;
            await context.Response.WriteAsync(errorMessage, Encoding.UTF8);
        }
    }
}