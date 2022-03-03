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

            services.AddMyDefaultCors(_config);

            var providers = services.BuildServiceProvider();
            providers.GetRequiredService<ICorsService>().Should().NotBeNull();
        }

        [Fact]
        public void Deve_Injetar_Autenticacao_Padrao()
        {
            var services = new Mock<IServiceCollection>().Object;
            Action act = () => services.AddMyDefaultAuthentication(_config);
            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Deve_Validar_As_Options_Do_JwtBearer()
        {
            JwtBearerOptions options = new();
            IServiceCollectionExtensions.GetJwtBearerOptions(_config).Invoke(options);

            options.SaveToken.Should().BeFalse();
            options.RequireHttpsMetadata.Should().BeFalse();
            options.TokenValidationParameters.Should().NotBeNull();
            options.TokenValidationParameters.ValidateIssuer.Should().BeTrue();
            options.TokenValidationParameters.ValidIssuer.Should().Be(_config["Auth:Issuer"]);
            options.TokenValidationParameters.ValidateAudience.Should().BeTrue();
            options.TokenValidationParameters.ValidAudience.Should().Be(_config["Auth:Audience"]);
            options.TokenValidationParameters.ValidateIssuerSigningKey.Should().BeTrue();
        }
    }
}