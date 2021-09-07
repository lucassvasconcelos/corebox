using CoreBox.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CoreBox.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<GlobalExceptionHandler>();
    }
}