using System.IO;
using CoreBox.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class ConfigurationExtensionsUnitTests
    {
        [Fact]
        public void Deve_Obter_O_AppSettings_Sem_Ambiente()
        {
            var env = new HostingEnvironment();
            env.ContentRootPath = Path.GetFullPath("../../../");
            env.EnvironmentName = "";

            var config = (new ConfigurationBuilder().Build()).GetEnvironmentConfiguration(env);
            config["Value"].Should().Be("Local");
        }

        [Fact]
        public void Deve_Obter_O_AppSettings_Development()
        {
            var env = new HostingEnvironment();
            env.ContentRootPath = Path.GetFullPath("../../../");
            env.EnvironmentName = "Development";

            var config = (new ConfigurationBuilder().Build()).GetEnvironmentConfiguration(env);
            config["Value"].Should().Be("Dev");
        }

        [Fact]
        public void Deve_Obter_O_AppSettings_Production()
        {
            var env = new HostingEnvironment();
            env.ContentRootPath = Path.GetFullPath("../../../");
            env.EnvironmentName = "Production";

            var config = (new ConfigurationBuilder().Build()).GetEnvironmentConfiguration(env);
            config["Value"].Should().Be("Prod");
        }
    }
}