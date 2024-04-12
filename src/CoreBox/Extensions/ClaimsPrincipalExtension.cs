using System.Security.Claims;

namespace CoreBox.Extensions;

public static class ClaimsPrincipalExtension
{
    public static bool HasRole(this ClaimsPrincipal claimsPrincipal, string[] roles)
    {
        foreach (var role in roles)
            if (claimsPrincipal.IsInRole(role))
                return true;

        return false;
    }

    public static Claim? GetUserId(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
}