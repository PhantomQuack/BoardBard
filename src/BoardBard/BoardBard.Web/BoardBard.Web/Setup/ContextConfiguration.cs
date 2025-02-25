using BoardBard.Core.Contexts;
using BoardBard.Core.Tools;
using Microsoft.EntityFrameworkCore;

namespace BoardBard.Web.Setup;

public static class ContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbProvider = configuration.GetValue<string>("BOARDBARD_DB_PROVIDER");
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<DataContext>>();
        ArgumentException.ThrowIfNullOrWhiteSpace(dbProvider, "Database provider is null or empty");
        logger.LogInformation("Using {Provider} database provider", dbProvider);
        switch (dbProvider)
        {
            case "sqlite":
                services.ConfigureSqlite(configuration, logger);
                break;
            case "postgres":
                services.ConfigurePostgres(configuration, logger);
                break;
            default:
                throw new NotSupportedException($"The database provider '{dbProvider}' is not supported.");
        }

        using var serviceProvider = services.BuildServiceProvider();
        using var dbContext = serviceProvider.GetRequiredService<DataContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        return services;
    }

    private static IServiceCollection ConfigureSqlite(this IServiceCollection services, IConfiguration configuration,
        ILogger<DataContext>? logger = null)
    {
        var connectionString = configuration.GetValue<string>("BOARDBARD_SQLITE_CONNECTION_STRING");
        var migrationsTable = configuration.GetValue<string>("BOARDBARD_SQLITE_MIGRATIONS_TABLE");
        var enableDetailedErrors = configuration.GetValue("BOARDBARD_SQLITE_ENABLE_DETAILED_ERRORS", false);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, "Connection string is null or empty");
        services.AddDbContext<SqliteDbContext>(options =>
        {
            options.UseSqlite(connectionString, sqliteOptions =>
            {
                if (!string.IsNullOrWhiteSpace(migrationsTable))
                {
                    logger?.LogInformation("Using migrations table {MigrationsTable}", migrationsTable);
                    sqliteOptions.MigrationsHistoryTable(migrationsTable);
                }

                if (enableDetailedErrors)
                    return;
                logger?.LogInformation("Enabling detailed errors");
                options.EnableDetailedErrors();
            });
        });
        services.AddScoped<DataContext>(provider => provider.GetRequiredService<SqliteDbContext>());
        return services;
    }

    private static IServiceCollection ConfigurePostgres(this IServiceCollection services, IConfiguration configuration,
        ILogger<DataContext>? logger = null)
    {
        var database = configuration.GetValue<string>("BOARDBARD_POSTGRES_DATABASE");
        var userName = configuration.GetValue<string>("BOARDBARD_POSTGRES_USERNAME");
        var password = configuration.GetValue<string>("BOARDBARD_POSTGRES_PASSWORD");
        var host = configuration.GetValue<string>("BOARDBARD_POSTGRES_HOST");
        var port = configuration.GetValue<int?>("BOARDBARD_POSTGRES_PORT");
        var connectionString = $"Host={host};" +
                               $"Port={port ?? 5432};" +
                               $"Database={database};" +
                               $"Username={userName};" +
                               $"Password={password}";
        ArgumentExceptionExtended.ThrowIfAnyNullOrWhiteSpace(database, userName, password);
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseNpgsql(connectionString, dbOptions =>
            {
                var migrationsTable = configuration.GetValue<string>("BOARDBARD_POSTGRES_MIGRATIONS_TABLE");
                var schema = configuration.GetValue<string>("BOARDBARD_POSTGRES_SCHEMA");
                if (string.IsNullOrWhiteSpace(migrationsTable))
                    return;
                logger?.LogInformation("Using migrations table {MigrationsTable}", migrationsTable);
                if (string.IsNullOrWhiteSpace(schema))
                    return;
                logger?.LogInformation("Using schema {Schema}", schema);
                dbOptions.MigrationsHistoryTable(migrationsTable,
                    string.IsNullOrWhiteSpace(schema) ? "Core" : schema);
            });
        });
        services.AddScoped<DataContext>(provider => provider.GetRequiredService<PostgresDbContext>());
        return services;
    }
}