using System;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests
{
    public class ValueObjectUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Deve_Ser_Diferente(string numeroDocumento, string othernumeroDocumento, DateTime dataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(othernumeroDocumento, dataEmissao);
            documento.Should().NotBe(outroDocumento);
        }

        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Deve_Ser_Diferente_Com_Operador(string numeroDocumento, string othernumeroDocumento, DateTime dataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(othernumeroDocumento, dataEmissao);
            (documento != outroDocumento).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Devem_Ter_HashCodes_Diferentes(string numeroDocumento, string outroNumeroDocumento, DateTime dataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(outroNumeroDocumento, dataEmissao);
            (documento.GetHashCode() != outroDocumento.GetHashCode()).Should().BeTrue();
        }
    }
}