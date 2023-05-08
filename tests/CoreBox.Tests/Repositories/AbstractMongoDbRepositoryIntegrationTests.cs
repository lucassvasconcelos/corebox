using System.Threading.Tasks;
using CoreBox.Test;
using EphemeralMongo;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CoreBox.Tests.Repositories;

public class AbstractMongoDbRepositoryIntegrationTests
{
    private readonly IMongoRunner _mongoRunner;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly ProdutoMongoDbRepository _produtoMongoDbRepository;

    public AbstractMongoDbRepositoryIntegrationTests()
    {
        _mockConfiguration = new Mock<IConfiguration>();
        _mongoRunner = MongoRunner.Run();

        var mockConfSection = new Mock<IConfigurationSection>();
        mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "MongoDbConnection")]).Returns(_mongoRunner.ConnectionString);
        _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

        _produtoMongoDbRepository = new ProdutoMongoDbRepository(_mockConfiguration.Object);
    }

    [Fact]
    public void Deve_Obter_Ou_Criar_O_Banco_De_Dados_CoreBox_No_MongoDb()
    {
        var coreboxDatabase = _produtoMongoDbRepository.GetOrCreateDatabase();
        coreboxDatabase.Should().NotBeNull();
        coreboxDatabase.DatabaseNamespace.DatabaseName.Should().Be("CoreBox");
    }

    [Theory, AutoMoqData]
    public async Task Deve_Inserir_Um_Produto(Produto produto)
    {
        await _produtoMongoDbRepository.AddAsync(produto);
        var p = await _produtoMongoDbRepository.FirstOrDefaultAsync(p => p.Id == produto.Id);

        p.Id.Should().Be(produto.Id);
        p.Nome.Should().Be(produto.Nome);
        p.Preco.Should().Be(produto.Preco);
    }

    [Theory, AutoMoqData]
    public async Task Deve_Deletar_Um_Produto(Produto produto)
    {
        await _produtoMongoDbRepository.AddAsync(produto);
        var p = await _produtoMongoDbRepository.FirstOrDefaultAsync(p => p.Id == produto.Id);

        p.Id.Should().Be(produto.Id);
        p.Nome.Should().Be(produto.Nome);
        p.Preco.Should().Be(produto.Preco);

        await _produtoMongoDbRepository.DeleteAsync(p => p.Id == p.Id);

        p = await _produtoMongoDbRepository.FirstOrDefaultAsync(p => p.Id == produto.Id);
        p.Should().BeNull();
    }
}