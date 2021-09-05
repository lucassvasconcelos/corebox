using System;
using CoreBox.Extensions;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Extensions.Validator
{
    public class ValidatorUnitTests
    {
        [Fact]
        public void Deve_Indicar_Que_A_Validacao_Falhou()
        {
            Action act = () => (new Foo()).ValidateAndThrow(new FooValidator());
            act.Should().ThrowExactly<ValidationException>();
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Nao_Assinalar_Erro_De_Validacao(Foo foo)
        {
            Action act = () => foo.ValidateAndThrow(new FooValidator());
            act.Should().NotThrow<ValidationException>();
        }
    }
}