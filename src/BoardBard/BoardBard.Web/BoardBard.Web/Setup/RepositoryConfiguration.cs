using BoardBard.Core.Contexts;
using BoardBard.Core.Factories;
using BoardBard.Core.Interfaces;
using BoardBard.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BoardBard.Web.Setup;

public static class RepositoryConfiguration
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbProvider = configuration.GetValue<string>("BOARDBARD_DB_PROVIDER");
        ArgumentException.ThrowIfNullOrWhiteSpace(dbProvider, "Database provider is null or empty");

        switch (dbProvider)
        {
            case "sqlite":
                services.ConfigureSqlite(configuration);
                break;
            default:
                throw new InvalidOperationException("Unsupported database provider");
        }

        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        return services;
    }

    private static void ConfigureSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("BOARDBARD_SQLITE_CONNECTION_STRING");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, "Connection string is null or empty");
        services.AddDbContext<SqliteDbContext>(options =>
            options.UseSqlite(connectionString, sqliteOptions =>
            {
                var migrationsTable = configuration.GetValue<string>("BOARDBARD_SQLITE_MIGRATIONS_TABLE");
                if (!string.IsNullOrWhiteSpace(migrationsTable))
                    sqliteOptions.MigrationsHistoryTable(migrationsTable);
                if (configuration.GetValue("BOARDBARD_SQLITE_ENABLE_DETAILED_ERRORS", false))
                    options.EnableDetailedErrors();
            }));
        
        using var serviceProvider = services.BuildServiceProvider();
        using var dbContext = serviceProvider.GetRequiredService<SqliteDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        services.AddScoped(typeof(IRepository<>), typeof(SqliteRepository<>));
    }
}