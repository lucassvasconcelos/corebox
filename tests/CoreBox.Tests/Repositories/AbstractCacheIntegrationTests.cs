using System;
using System.Threading.Tasks;
using CoreBox.Tests.Repositories.FakeObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoreBox.Tests.Repositories
{
    public class AbstractCacheIntegrationTests
    {
        private readonly Context _context;
        private readonly DbContextOptions<Context> _contextOptions;
        private readonly CacheRepository _cacheRepository;

        public AbstractCacheIntegrationTests()
        {
            _contextOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new Context(_contextOptions);

            _cacheRepository = new CacheRepository(_context);
        }

        [Fact]
        public async Task Deve_Salvar_E_Obter_Um_Produto_No_Cache()
        {
            var produto = Produto.Criar("Produto1", 100);

            await _cacheRepository.SaveAsync<Produto>("Produto1", produto);

            var p = await _cacheRepository.GetAsync<ProdutoCacheModel>("Produto1");

            ProdutoCacheModel.ToEntity(p).Should().Be(produto);
        }

        [Fact]
        public async Task Deve_Salvar_E_Obter_E_Deletar_Um_Produto_No_Cache()
        {
            var produto = Produto.Criar("Produto2", 200);

            await _cacheRepository.SaveAsync<Produto>("Produto2", produto);

            var p = await _cacheRepository.GetAsync<ProdutoCacheModel>("Produto2");

            ProdutoCacheModel.ToEntity(p).Should().Be(produto);

            await _cacheRepository.RemoveAsync("Produto2");

            p = await _cacheRepository.GetAsync<ProdutoCacheModel>("Produto2");

            p.Should().Be(null);
        }

        [Fact]
        public void Deve_Reportar_Erro_Por_Falta_De_Informacoes_Ao_Salvar()
        {
            Func<Task> act = async () => await _cacheRepository.SaveAsync<Produto>("", null);
            act.Should().ThrowExactly<ArgumentException>("Não foi possível adicionar informação do cache: Chave ou Objeto não informado");
        }

        [Fact]
        public void Deve_Reportar_Erro_Por_Falta_De_Informacoes_Ao_Remover()
        {
            Func<Task> act = async () => await _cacheRepository.RemoveAsync("");
            act.Should().ThrowExactly<ArgumentException>("Não foi possível remover informação do cache: Chave não informada");
        }

        [Fact]
        public void Deve_Reportar_Erro_Por_Falta_De_Informacoes_Ao_Obter()
        {
            Func<Task> act = async () => await _cacheRepository.GetAsync<Produto>("");
            act.Should().ThrowExactly<ArgumentException>("Não foi possível obter informação do cache: Chave não informada");
        }
    }
}