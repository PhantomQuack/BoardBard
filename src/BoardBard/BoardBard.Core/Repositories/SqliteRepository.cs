using BoardBard.Core.Contexts;
using BoardBard.Core.Implementations;

namespace BoardBard.Core.Repositories;

public class SqliteRepository<T>(SqliteDbContext context) : RepositoryBase<T>(context)
    where T : class
{
    
}