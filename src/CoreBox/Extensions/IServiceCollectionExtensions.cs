using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreBox.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddMyDefaultControllers(this IServiceCollection services)
        => services.AddControllers().AddJsonOptions(opts
            => opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    public static void AddMyDefaultCors(this IServiceCollection services, string allowedOrigins, string exposedHeaders)
        => services.AddCors(opts =>
        {
            opts.AddPolicy("MyDefaultCors", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithOrigins(allowedOrigins.Split(';'));
                policy.SetIsOriginAllowedToAllowWildcardSubdomains();
                policy.AllowCredentials();
                policy.WithExposedHeaders(exposedHeaders.Split(';'));
            });
        });
}