using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreBox.Middlewares;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CoreBox.Tests.Middlewares
{
    public class GlobalExceptionHandlerUnitTests
    {
        [Fact]
        public async Task GivenWebApp_WhenFirstRequestIsSentToWebApp_ThenDummyCookieIsAddedToResponse()
        {
            const string expectedOutput = "Erro de validação";
            var exceptionHandler = new ExceptionHandlerFeature();
            exceptionHandler.Error = new ValidationException(expectedOutput);

            DefaultHttpContext defaultContext = new DefaultHttpContext();
            defaultContext.Features.Set<IExceptionHandlerFeature>(exceptionHandler);
            defaultContext.Request.Path = "/";

            var middlewareInstance = new GlobalExceptionHandler(async (httpContext) => await Task.CompletedTask);
            await middlewareInstance.ExecuteAsync(defaultContext);

            defaultContext.Response.ContentLength.Should().Be(expectedOutput.Length);
            defaultContext.Response.StatusCode.Should().Be(400);
            defaultContext.Response.ContentType.Should().Be("application/json");
        }
    }
}