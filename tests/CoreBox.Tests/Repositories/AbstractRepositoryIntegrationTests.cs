using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBox.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoreBox.Tests.Repositories
{
    public class AbstractRepositoryIntegrationTests
    {
        private readonly Context _context;
        private readonly DbContextOptions<Context> _contextOptions;
        private readonly IUnitOfWork _unitOfWork;

        public AbstractRepositoryIntegrationTests()
        {
            _contextOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new Context(_contextOptions);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Criar_E_Persistir_Um_Produto(Produto produto)
        {
            await _unitOfWork.GetRepository<Produto>().SaveAsync(produto);
            await _unitOfWork.CommitAsync();

            var results = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results.Count().Should().Be(1);
            results.FirstOrDefault().Id.Should().Be(produto.Id);
            results.FirstOrDefault().DataCriacao.Should().Be(produto.DataCriacao);
            results.FirstOrDefault().DataUltimaAtualizacao.Should().BeNull();
            results.FirstOrDefault().Nome.Should().Be(produto.Nome);
            results.FirstOrDefault().Preco.Should().Be(produto.Preco);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Criar_E_Persistir_Uma_Lista_De_Produtos(List<Produto> produtos)
        {
            await _unitOfWork.GetRepository<Produto>().SaveRangeAsync(produtos);
            await _unitOfWork.CommitAsync();

            var results = (await _unitOfWork.GetRepository<Produto>().GetAllAsync()).ToList();
            results.Count().Should().Be(3);

            for (int i = 0; i < produtos.Count(); i++)
            {
                results[i].Id.Should().Be(produtos[i].Id);
                results[i].DataCriacao.Should().Be(produtos[i].DataCriacao);
                results[i].DataUltimaAtualizacao.Should().BeNull();
                results[i].Nome.Should().Be(produtos[i].Nome);
                results[i].Preco.Should().Be(produtos[i].Preco);
            }
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Criar_E_Persistir_Um_Produto_E_Atualizalo(Produto produto)
        {
            await _unitOfWork.GetRepository<Produto>().SaveAsync(produto);
            await _unitOfWork.CommitAsync();

            var result = await _unitOfWork.GetRepository<Produto>().GetByIdAsync(produto.Id);
            result.Id.Should().Be(produto.Id);
            result.DataCriacao.Should().Be(produto.DataCriacao);
            result.DataUltimaAtualizacao.Should().BeNull();
            result.Nome.Should().Be(produto.Nome);
            result.Preco.Should().Be(produto.Preco);

            Produto.AtualizarPreco(result, (result.Preco + 50));

            await _unitOfWork.GetRepository<Produto>().UpdateAsync(result);
            await _unitOfWork.CommitAsync();

            var result2 = await _unitOfWork.GetRepository<Produto>().GetByIdAsync(produto.Id);
            result2.Id.Should().Be(produto.Id);
            result2.DataCriacao.Should().Be(produto.DataCriacao);
            result2.DataUltimaAtualizacao.Should().BeNull();
            result2.Nome.Should().Be(produto.Nome);
            result2.Preco.Should().Be(produto.Preco + 50);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Deletar_Um_Produto(Produto produto1, Produto produto2, Produto produto3)
        {
            await _unitOfWork.GetRepository<Produto>().SaveRangeAsync(new List<Produto> { produto1, produto2, produto3 });
            await _unitOfWork.CommitAsync();

            var results = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results.Count().Should().Be(3);

            foreach (var item in results)
                _context.Entry(item).State = EntityState.Detached;

            await _unitOfWork.GetRepository<Produto>().DeleteAsync(produto1);
            await _unitOfWork.CommitAsync();

            var results2 = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results2.Count().Should().Be(2);

            results2.FirstOrDefault(produto => produto.Id == produto1.Id).Should().BeNull();
            results2.FirstOrDefault(produto => produto.Id == produto2.Id).Should().NotBeNull();
            results2.FirstOrDefault(produto => produto.Id == produto3.Id).Should().NotBeNull();
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Deletar_Todos_Os_Produtos_Cadastrados(IEnumerable<Produto> produtos)
        {
            await _unitOfWork.GetRepository<Produto>().SaveRangeAsync(produtos);
            await _unitOfWork.CommitAsync();

            var results = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results.Count().Should().Be(3);

            foreach (var item in results)
                _context.Entry(item).State = EntityState.Detached;

            await _unitOfWork.GetRepository<Produto>().DeleteRangeAsync(produtos);
            await _unitOfWork.CommitAsync();

            var results2 = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results2.Count().Should().Be(0);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Obter_Um_Produto_Por_Specification(IEnumerable<Produto> produtos)
        {
            await _unitOfWork.GetRepository<Produto>().SaveRangeAsync(produtos);
            await _unitOfWork.CommitAsync();

            var results = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results.Count().Should().Be(3);

            var produto1 = results[0];
            var produto2 = results[1];
            var produto3 = results[2];

            Produto.AtualizarPreco(produto1, 10);
            Produto.AtualizarPreco(produto2, 20);
            Produto.AtualizarPreco(produto3, 30);

            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto1);
            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto2);
            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto3);
            await _unitOfWork.CommitAsync();

            var results2 = await _unitOfWork.GetRepository<Produto>().GetAsync(new ProdutoComPrecoAbaixoSpecification(15));
            results2.Should().Be(produto1);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Obter_Varios_Produtos_Por_Specification(IEnumerable<Produto> produtos)
        {
            await _unitOfWork.GetRepository<Produto>().SaveRangeAsync(produtos);
            await _unitOfWork.CommitAsync();

            var results = await _unitOfWork.GetRepository<Produto>().GetAllAsync();
            results.Count().Should().Be(3);

            var produto1 = results[0];
            var produto2 = results[1];
            var produto3 = results[2];

            Produto.AtualizarPreco(produto1, 10);
            Produto.AtualizarPreco(produto2, 20);
            Produto.AtualizarPreco(produto3, 30);

            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto1);
            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto2);
            await _unitOfWork.GetRepository<Produto>().UpdateAsync(produto3);
            await _unitOfWork.CommitAsync();

            var results2 = await _unitOfWork.GetRepository<Produto>().GetAllAsync(new ProdutoComPrecoAbaixoSpecification(25));
            results2.Count().Should().Be(2);
            results2.First(f => f.Id == produto1.Id).Should().Be(produto1);
            results2.First(f => f.Id == produto2.Id).Should().Be(produto2);
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Validar_Que_Existe_Um_Produto_Por_Id(Produto produto)
        {
            await _unitOfWork.GetRepository<Produto>().SaveAsync(produto);
            await _unitOfWork.CommitAsync();

            var result = await _unitOfWork.GetRepository<Produto>().AnyAsync(new ProdutoPorIdSpecification(produto.Id));

            result.Should().BeTrue();
        }

        [Theory, AutoMoqDataAttribute]
        public async Task Deve_Validar_Que_Nao_Existe_Um_Produto_Por_Id(Produto produto)
        {
            await _unitOfWork.GetRepository<Produto>().SaveAsync(produto);
            await _unitOfWork.CommitAsync();

            var result = await _unitOfWork.GetRepository<Produto>().AnyAsync(new ProdutoPorIdSpecification(Guid.NewGuid()));

            result.Should().BeFalse();
        }
    }
}