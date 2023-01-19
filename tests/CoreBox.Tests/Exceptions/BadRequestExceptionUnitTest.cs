using CoreBox.Exceptions;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions;

public class BadRequestExceptionUnitTest
{
    [Theory, AutoMoqDataAttribute]
    public void Deve_Criar_Um_BadRequestException_Valido(string message)
    {
        var ex = new BadRequestException(message);
        ex.Message.Should().Be(message);
    }
}