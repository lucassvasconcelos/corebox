using System;
using System.Collections.Generic;
using System.Linq;
using CoreBox.Application;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Application;

public class ApplicationResponseUnitTests
{
    [Fact]
    public void Deve_Gerar_Uma_Resposta_De_Sucesso()
    {
        var response = ApplicationResponse<int>.Success(200, 1);
        response.StatusCode.Should().Be(200);
        response.Data.Should().Be(1);
        response.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(400)]
    [InlineData(500)]
    public void Deve_Gerar_Uma_Exception_Caso_Resposta_De_Sucesso_Tenha_HttpStatus_Invalido(int statusCode)
    {
        Action act = () => ApplicationResponse<int>.Success(statusCode, 1);
        act.Should().ThrowExactly<Exception>();
    }

    [Fact]
    public void Deve_Gerar_Uma_Resposta_De_Falha()
    {
        var response = ApplicationResponse<object>.Fail(400, "Erro");
        response.StatusCode.Should().Be(400);
        response.Data.Should().BeNull();
        response.Errors.Should().HaveCount(1);
        response.Errors.First().Should().Be("Erro");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(200)]
    [InlineData(201)]
    [InlineData(300)]
    public void Deve_Gerar_Uma_Exception_Caso_Resposta_De_Falha_Tenha_HttpStatus_Invalido(int statusCode)
    {
        Action act = () => ApplicationResponse<object>.Fail(statusCode, "Erro");
        act.Should().ThrowExactly<Exception>();
    }

    [Fact]
    public void Deve_Gerar_Uma_Resposta_De_Falha_Com_Erros()
    {
        var response = ApplicationResponse<object>.FailWithErrors(400, new List<string> { "Erro 1", "Erro 2" });
        response.StatusCode.Should().Be(400);
        response.Data.Should().BeNull();
        response.Errors.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(200)]
    [InlineData(201)]
    [InlineData(300)]
    public void Deve_Gerar_Uma_Exception_Caso_Resposta_De_Falha_Com_Erros_Tenha_HttpStatus_Invalido(int statusCode)
    {
        Action act = () => ApplicationResponse<object>.FailWithErrors(statusCode, new List<string> { "Erro 1", "Erro 2" });
        act.Should().ThrowExactly<Exception>();
    }
}