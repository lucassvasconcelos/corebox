using System;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Validations;

public class CpfValidatorUnitTests
{
    [Theory]
    [InlineData(true, "Pessoa Física", "12345678900", "O CPF é inválido")]
    [InlineData(true, "Pessoa Física", "11111111111", "O CPF é inválido")]
    [InlineData(true, "Pessoa Física", "", "O CPF é inválido")]
    [InlineData(true, "Pessoa Física", null, "O CPF é inválido")]
    [InlineData(false, "Pessoa Física", "12345678909", "")]
    public void Deve_Validar_Cpf_Da_Pessoa(bool shouldThrow, string tipoPessoa, string documento, string errorMessage)
    {
        Action act = () => new Pessoa(tipoPessoa, documento);

        if (shouldThrow)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage));
        else
            act.Should().NotThrow();
    }
}