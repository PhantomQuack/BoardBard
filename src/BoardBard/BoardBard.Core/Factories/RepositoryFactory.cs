using BoardBard.Core.Interfaces;
using BoardBard.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardBard.Core.Factories;

public class RepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
{
    public IRepository<T> CreateRepository<T>() where T : class
    {
        var provider = serviceProvider.GetRequiredService<IConfiguration>().GetValue<string>("BOARDBARD_DB_PROVIDER");

        return provider switch
        {
            "sqlite" => serviceProvider.GetRequiredService<SqliteRepository<T>>(),
            _ => throw new InvalidOperationException("Unsupported database provider"),
        };
    }
}