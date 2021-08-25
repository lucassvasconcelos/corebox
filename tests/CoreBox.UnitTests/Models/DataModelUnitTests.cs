using System;
using FluentAssertions;
using Xunit;

namespace CoreBox.UnitTests
{
    public class DataModelUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Uma_Instancia_De_DataModel(Guid id)
        {
            var pessoaDataModel = new PessoaDataModel();
            pessoaDataModel.Id = id;
            pessoaDataModel.DataCriacao = DateTime.Now;

            pessoaDataModel.Should().NotBeNull();
            pessoaDataModel.DataUltimaAtualizacao.Should().BeNull();
        }
    }
}