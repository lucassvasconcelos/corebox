using CoreBox.Exceptions;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class ForbiddenExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_ForbiddenException_Valida(string message)
        {
            var ex = new ForbiddenException(message);
            ex.Message.Should().Be(message);
        }
    }
}