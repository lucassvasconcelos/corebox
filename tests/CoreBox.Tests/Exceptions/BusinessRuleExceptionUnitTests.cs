using CoreBox.Exceptions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Exceptions
{
    public class BusinessRuleExceptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Um_BusinessRuleException_Valida(string message)
        {
            var ex = new BusinessRuleException(message);
            ex.Message.Should().Be(message);
        }
    }
}