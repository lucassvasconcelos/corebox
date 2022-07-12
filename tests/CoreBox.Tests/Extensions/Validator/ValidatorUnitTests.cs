using System;
using CoreBox.Extensions;
using CoreBox.Test;
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

        [Fact]
        public void Deve_Lancar_Uma_Excecao_Pelo_Objeto_Estar_Nulo()
        {
            Foo foo = null;
            Action act = () => foo.ValidateNullAndThrow("Foo is null");
            act.Should().ThrowExactly<ValidationException>().WithMessage("Foo is null");
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Nao_Lancar_Uma_Excecao_Pelo_Objeto_Estar_Nulo(Foo foo)
        {
            Action act = () => foo.ValidateNullAndThrow("Foo is null");
            act.Should().NotThrow<ValidationException>();
        }
    }
}