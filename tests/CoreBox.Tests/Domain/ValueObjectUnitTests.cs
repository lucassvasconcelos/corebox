using System;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests
{
    public class ValueObjectUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Deve_Ser_Igual(string numeroDocumento, DateTime dataEmissao, DateTime outraDataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(numeroDocumento, outraDataEmissao);
            documento.Should().Be(outroDocumento);
            documento.Equals(outroDocumento).Should().BeTrue();

            documento = (Documento)null;
            (documento == outroDocumento).Should().BeFalse();
            outroDocumento.Equals(documento).Should().BeFalse();

            documento = (Documento)null;
            outroDocumento = documento;
            (documento == outroDocumento).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Deve_Ser_Igual_Com_Operador(string numeroDocumento, DateTime dataEmissao, DateTime outraDataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(numeroDocumento, outraDataEmissao);
            (documento == outroDocumento).Should().BeTrue();
        }

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
        public void ObjetoDeValor_Devem_Ter_HashCodes_Iguais(string numeroDocumento, DateTime dataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var mesmoDocumento = documento;
            (documento.GetHashCode() == mesmoDocumento.GetHashCode()).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void ObjetoDeValor_Devem_Ter_HashCodes_Iguais_Por_Causa_Do_Numero_Do_Documento(string numeroDocumento, DateTime dataEmissao)
        {
            var documento = Documento.Criar(numeroDocumento, dataEmissao);
            var outroDocumento = Documento.Criar(numeroDocumento, dataEmissao);
            (documento.GetHashCode() == outroDocumento.GetHashCode()).Should().BeTrue();
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