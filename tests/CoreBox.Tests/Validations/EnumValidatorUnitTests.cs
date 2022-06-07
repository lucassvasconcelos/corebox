using System;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Validations;

public class EnumValidatorUnitTests
{
    [Theory]
    [InlineData(true, "Pessoa Física", "12345678909", 0, "Sexo inválido!")]
    [InlineData(true, "Pessoa Física", "12345678909", 15, "Sexo inválido!")]
    [InlineData(true, "Pessoa Física", "12345678909", null, "Sexo inválido!")]
    public void Deve_Validar_Enum_Sexo_Da_Pessoa(bool shouldThrow, string tipoPessoa, string documento, int? sexo, string errorMessage)
    {
        Action act = () => new Pessoa(tipoPessoa, documento, sexo);

        if (shouldThrow)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage));
        else
            act.Should().NotThrow();
    }
}