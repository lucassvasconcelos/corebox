using System;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Validations;

public class CnpjValidatorUnitTests
{
    [Theory]
    [InlineData(true, "Pessoa Jurídica", "12345678900", "O CNPJ é inválido")]
    [InlineData(true, "Pessoa Jurídica", "11111111111111", "O CNPJ é inválido")]
    [InlineData(true, "Pessoa Jurídica", "", "O CNPJ é inválido")]
    [InlineData(true, "Pessoa Jurídica", null, "O CNPJ é inválido")]
    [InlineData(false, "Pessoa Jurídica", "15287312000103", "")]
    public void Deve_Validar_Cpf_Da_Pessoa(bool shouldThrow, string tipoPessoa, string documento, string errorMessage)
    {
        Action act = () => new Pessoa(tipoPessoa, documento);

        if (shouldThrow)
            act.Should().ThrowExactly<ValidationException>().Where(w => w.Message.Contains(errorMessage));
        else
            act.Should().NotThrow();
    }
}