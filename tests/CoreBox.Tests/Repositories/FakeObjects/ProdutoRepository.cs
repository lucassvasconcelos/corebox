using System;
using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreBox.Tests.Repositories
{
    public class ProdutoRepository : AbstractRepository<Produto>, IRepository<Produto>
    {
        public ProdutoRepository(Context context) : base(context.Produtos, context)
        {
        }
    }
}