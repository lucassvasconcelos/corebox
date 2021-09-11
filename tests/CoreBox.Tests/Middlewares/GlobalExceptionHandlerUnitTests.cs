using System.IO;
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
        public async Task Deve_Simular_A_Execucao_Do_Middleware_Para_Um_Validation_Exception()
        {
            const string expectedOutput = "Erro de validação";
            var exceptionHandler = new ExceptionHandlerFeature();
            exceptionHandler.Error = new ValidationException(expectedOutput);

            var defaultContext = new DefaultHttpContext();
            defaultContext.Features.Set<IExceptionHandlerFeature>(exceptionHandler);
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            var middlewareInstance = new GlobalExceptionHandler(async (httpContext) => await Task.CompletedTask);

            await middlewareInstance.InvokeAsync(defaultContext);

            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();

            body.Should().Be(expectedOutput);
            defaultContext.Response.ContentLength.Should().Be(expectedOutput.Length);
            defaultContext.Response.StatusCode.Should().Be(400);
            defaultContext.Response.ContentType.Should().Be("application/json");
        }

        [Fact]
        public async Task Deve_Simular_A_Execucao_Do_Middleware_Para_Um_Erro_Nao_Identificado()
        {
            var defaultContext = new DefaultHttpContext();
            defaultContext.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature());
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            var middlewareInstance = new GlobalExceptionHandler(async (httpContext) => await Task.CompletedTask);

            await middlewareInstance.InvokeAsync(defaultContext);

            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();

            body.Should().Be("");
            defaultContext.Response.ContentLength.Should().Be(0);
            defaultContext.Response.StatusCode.Should().Be(500);
            defaultContext.Response.ContentType.Should().Be("application/json");
        }
    }
}