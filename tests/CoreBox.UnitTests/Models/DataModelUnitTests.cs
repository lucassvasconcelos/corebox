using System;
using FluentAssertions;
using Xunit;

namespace CoreBox.UnitTests
{
    public class DataModelUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Uma_Instancia_De_PessoaDataModel(
            Guid id,
            DateTime dataCriacao,
            DateTime dataUltimaAtualizacao,
            string nome,
            Documento documento
        )
        {
            var p = new PessoaDataModel
            {
                Id = id,
                DataCriacao = dataCriacao,
                DataUltimaAtualizacao = dataUltimaAtualizacao,
                Nome = nome,
                Documento = documento
            };

            p.Should().NotBeNull();
            p.Id.Should().Be(id);
            p.DataCriacao.Should().Be(dataCriacao);
            p.DataUltimaAtualizacao.Should().Be(dataUltimaAtualizacao);
            p.Nome.Should().Be(nome);
            p.Documento.Should().Be(documento);
        }
    }
}