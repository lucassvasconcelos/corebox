using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CoreBox.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration GetEnvironmentConfiguration(this IConfiguration configuration, IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return configurationBuilder.Build();
        }
    }
}