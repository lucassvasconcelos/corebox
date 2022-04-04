using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CoreBox.Extensions;

public static class ConfigurationExtensions
{
    public static IConfiguration GetEnvironmentConfiguration(this IConfiguration configuration, IHostEnvironment env)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}