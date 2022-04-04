using CoreBox.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CoreBox.Extensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseExceptionHandler(builder
            => builder.Run(async context
                => await GlobalExceptionHandler.InvokeAsync(context)));
}