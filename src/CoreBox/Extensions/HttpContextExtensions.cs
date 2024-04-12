using Microsoft.AspNetCore.Http;

namespace CoreBox.Extensions;

public static class HttpContextExtensions
{
    public static bool IsAuthenticated(this HttpContext httpContext)
        => httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated;

    public static string GetUserIdAsString(this HttpContext httpContext)
    {
        if (!IsAuthenticated(httpContext))
            throw new UnauthorizedAccessException("Permissão negada!");

        var claim = httpContext.User.GetUserId();

        if (claim is null)
            throw new UnauthorizedAccessException("Permissão negada!");

        return claim.Value;
    }
}