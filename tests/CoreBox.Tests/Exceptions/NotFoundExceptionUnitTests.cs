using CoreBox.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class NotFoundExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_NotFoundException_Valida(string message)
        {
            var ex = new NotFoundException(message);
            ex.Message.Should().Be(message);
        }
    }
}