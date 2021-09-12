using CoreBox.Repositories;

namespace CoreBox.Tests.Repositories
{
    public class ProdutoRepository : AbstractRepository<Produto>, IRepository<Produto>
    {
        public ProdutoRepository(Context context) : base(context.Produtos, context)
        {
        }
    }
}