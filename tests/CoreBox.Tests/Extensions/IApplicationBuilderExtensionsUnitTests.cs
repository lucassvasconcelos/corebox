using System.Diagnostics;
using CoreBox.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class IApplicationBuilderExtensionsUnitTests
    {
        [Fact]
        public void Deve_Injetar_O_Middleware_No_Application_Builder(
        )
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddSingleton<DiagnosticListener>(new DiagnosticListener("corebox"));

            var appBuilder = new ApplicationBuilder(services.BuildServiceProvider());
            appBuilder.UseGlobalExceptionHandler();
            var app = appBuilder.Build();

            app.Target.Should().BeOfType<ExceptionHandlerMiddleware>();
        }
    }
}