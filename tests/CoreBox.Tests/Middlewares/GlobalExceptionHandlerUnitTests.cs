using System.IO;
using System.Threading.Tasks;
using CoreBox.Middlewares;
using CoreBox.Types;
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
            const string expectedOutput = "{\"Error\":\"Erro de valida\\u00E7\\u00E3o\"}";
            var exceptionHandler = new ExceptionHandlerFeature();
            exceptionHandler.Error = new ValidationException("Erro de validação");

            var defaultContext = new DefaultHttpContext();
            defaultContext.Features.Set<IExceptionHandlerFeature>(exceptionHandler);
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            await GlobalExceptionHandler.InvokeAsync(defaultContext);

            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();

            body.Should().Be(expectedOutput);
            defaultContext.Response.ContentLength.Should().Be(expectedOutput.Length);
            defaultContext.Response.StatusCode.Should().Be(400);
            defaultContext.Response.ContentType.Should().Be(MimeType.json);
        }

        [Fact]
        public async Task Deve_Simular_A_Execucao_Do_Middleware_Para_Um_Erro_Nao_Identificado()
        {
            var defaultContext = new DefaultHttpContext();
            defaultContext.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature());
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            await GlobalExceptionHandler.InvokeAsync(defaultContext);

            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();

            body.Should().Be("{\"Error\":null}");
            defaultContext.Response.ContentLength.Should().Be("{\"Error\":null}".Length);
            defaultContext.Response.StatusCode.Should().Be(500);
            defaultContext.Response.ContentType.Should().Be(MimeType.json);
        }
    }
}