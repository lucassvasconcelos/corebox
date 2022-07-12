using CoreBox.Exceptions;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class ConflictExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_ConflictException_Valida(string message)
        {
            var ex = new ConflictException(message);
            ex.Message.Should().Be(message);
        }
    }
}