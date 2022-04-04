using Microsoft.EntityFrameworkCore;

namespace CoreBox.Repositories;

public interface IDbContext<T> where T : DbContext
{
    
}