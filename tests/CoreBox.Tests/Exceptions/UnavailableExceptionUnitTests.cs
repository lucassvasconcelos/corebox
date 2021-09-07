using CoreBox.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class UnavailableExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_UnavailableException_Valida(string message)
        {
            var ex = new UnavailableException(message);
            ex.Message.Should().Be(message);
        }
    }
}