using System;
using CoreBox.Test;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests
{
    public class EntityUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Igual(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = pessoa;
            pessoa.Should().Be(outraPessoa);
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Igual_Com_Operador(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = pessoa;
            (pessoa == outraPessoa).Should().BeTrue();

            pessoa = (Pessoa)null;
            (pessoa == outraPessoa).Should().BeFalse();

            pessoa = (Pessoa)null;
            outraPessoa = pessoa;
            (pessoa == outraPessoa).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Igual_Com_Operador_Inverso(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = pessoa;
            (pessoa != outraPessoa).Should().BeFalse();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_diferente_Com_Uma_Instancia_Nula_E_Uma_Instancia_Real(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            Pessoa pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            Pessoa outraPessoa = null;
            pessoa.Equals(outraPessoa).Should().BeFalse();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Diferente(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            pessoa.Should().NotBe(outraPessoa);
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Diferente_Com_Operador(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            (pessoa != outraPessoa).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidade_Deve_Ser_Diferente_Com_Operador_Inverso(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            (pessoa == outraPessoa).Should().BeFalse();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidades_Devem_Ter_HashCodes_Iguais(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var mesmaPessoa = pessoa;
            (pessoa.GetHashCode() == mesmaPessoa.GetHashCode()).Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public void Entidades_Devem_Ter_HashCodes_Diferentes(string nomePessoa, string numeroDocumento, DateTime dataEmissao)
        {
            var pessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            var outraPessoa = Pessoa.Criar(nomePessoa, numeroDocumento, dataEmissao);
            (pessoa.GetHashCode() != outraPessoa.GetHashCode()).Should().BeTrue();
        }
    }
}