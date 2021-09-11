using CoreBox.Extensions;
using CoreBox.Middlewares;
using FluentAssertions;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class IApplicationBuilderExtensionsUnitTests
    {
        [Fact]
        public void Deve_Injetar_O_Middleware_No_Application_Builder()
        {
            var serviceCollection = new ServiceCollection();

            var applicationBuilder = new ApplicationBuilder(serviceCollection.BuildServiceProvider());
            applicationBuilder.UseGlobalExceptionHandler();

            var app = applicationBuilder.Build();
            app.Target.Should().BeOfType<GlobalExceptionHandler>();
        }
    }
}