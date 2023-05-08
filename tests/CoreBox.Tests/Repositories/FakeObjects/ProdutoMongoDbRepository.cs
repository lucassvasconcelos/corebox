using CoreBox.Repositories;
using Microsoft.Extensions.Configuration;

namespace CoreBox.Tests.Repositories
{
    public class ProdutoMongoDbRepository : AbstractMongoDbRepository<Produto>
    {
        public ProdutoMongoDbRepository(IConfiguration _configuration)
            : base(_configuration, databaseName: "CoreBox", collectionName: "Produtos")
        {
        }
    }
}