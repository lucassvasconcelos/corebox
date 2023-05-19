using System;
using System.IO;
using CoreBox.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class IServiceCollectionExtensionsUnitTests
    {
        private readonly IConfiguration _config;

        public IServiceCollectionExtensionsUnitTests()
        {
            var env = new HostEnvironment();
            env.ContentRootPath = Path.GetFullPath("../../../");
            env.EnvironmentName = "";

            _config = (new ConfigurationBuilder().Build()).GetEnvironmentConfiguration(env);
        }

        [Fact]
        public void Deve_Injetar_Controllers_Padrao()
        {
            var services = new Mock<IServiceCollection>().Object;
            Action act = () => services.AddMyDefaultControllers();
            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Deve_Injetar_Cors_Padrao()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory>(LoggerFactory.Create(log => log.AddConsole()));

            services.AddMyDefaultCors(allowedOrigins: "Origin1;Origin2", exposedHeaders: "");

            var providers = services.BuildServiceProvider();
            providers.GetRequiredService<ICorsService>().Should().NotBeNull();
        }
    }
}