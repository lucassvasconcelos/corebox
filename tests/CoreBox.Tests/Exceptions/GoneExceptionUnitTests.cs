using CoreBox.Exceptions;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class GoneExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_GoneException_Valida(string message)
        {
            var ex = new GoneException(message);
            ex.Message.Should().Be(message);
        }
    }
}