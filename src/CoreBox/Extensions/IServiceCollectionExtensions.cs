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

    public static void AddMyDefaultCors(this IServiceCollection services, IConfiguration configuration)
        => services.AddCors(opts =>
        {
            opts.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithOrigins(configuration["AllowedOrigins"].Split(';'));
                policy.SetIsOriginAllowedToAllowWildcardSubdomains();
                policy.AllowCredentials();
                policy.WithExposedHeaders(configuration["ExposedHeaders"].Split(';'));
            });
        });

    public static void AddMyDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(GetJwtBearerOptions(configuration));

    public static Action<JwtBearerOptions> GetJwtBearerOptions(IConfiguration configuration)
        => opts =>
        {
            opts.SaveToken = false;
            opts.RequireHttpsMetadata = false;
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Auth:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Auth:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Auth:SecretKey"]))
            };
        };
}