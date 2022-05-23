using System;
using System.Linq;
using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions.Enum;

public class EnumExtensionsUnitTests
{
    [Theory]
    [InlineData(TipoPessoa.Fisica, "Pessoa Física")]
    [InlineData(TipoPessoa.Juridica, "Pessoa Jurídica")]
    public void Deve_Obter_A_Descricao_Do_Enum(TipoPessoa tipoPessoa, string resultado)
        => tipoPessoa.GetDescription().Should().Be(resultado);

    [Fact]
    public void Deve_Obter_Todas_As_Descricoes_Do_Enum()
    {
        var descriptions = typeof(TipoPessoa).GetDescriptions();
        descriptions.Should().HaveCount(2);
        descriptions.Count(d => d == "Pessoa Física").Should().Be(1);
        descriptions.Count(d => d == "Pessoa Jurídica").Should().Be(1);
    }

    [Fact]
    public void Deve_Nao_Obter_Todas_As_Descricoes_Pois_O_Tipo_Informado_Nao_E_Um_Enum()
        => typeof(Pessoa).GetDescriptions().Should().BeNull();

    [Theory]
    [InlineData("Pessoa Física", TipoPessoa.Fisica)]
    [InlineData("Pessoa Jurídica", TipoPessoa.Juridica)]
    public void Deve_Obter_O_Valor_Do_Enum_Pela_Descricao(string descricao, TipoPessoa resultado)
        => descricao.GetValueFromDescription<TipoPessoa>().Should().Be(resultado);

    [Fact]
    public void Deve_Nao_Obter_O_Valor_Do_Enum_Pela_Descricao()
    {
        Action act = () => "Teste".GetValueFromDescription<TipoPessoa>();
        act.Should().ThrowExactly<ArgumentException>().Where(w => w.Message.Contains($"O item Teste não foi encontrado"));
    }
}