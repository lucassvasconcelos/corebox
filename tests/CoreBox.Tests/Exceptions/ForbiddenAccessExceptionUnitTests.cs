using CoreBox.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class ForbiddenAccessExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_ForbiddenAccessException_Valida(string message)
        {
            var ex = new ForbiddenAccessException(message);
            ex.Message.Should().Be(message);
        }
    }
}