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
        ArgumentException.ThrowIfNullOrWhiteSpace(dbProvider, "Database provider is null or empty");
        switch (dbProvider)
        {
            case "sqlite":
                services.ConfigureSqlite(configuration);
                break;
            case "postgres":
                services.ConfigurePostgres(configuration);
                break;
        }

        using var serviceProvider = services.BuildServiceProvider();
        using var dbContext = serviceProvider.GetRequiredService<DataContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        return services;
    }

    private static IServiceCollection ConfigureSqlite(this IServiceCollection services, IConfiguration configuration)
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
                    sqliteOptions.MigrationsHistoryTable(migrationsTable);
                if (enableDetailedErrors)
                    options.EnableDetailedErrors();
            });
        });
        services.AddScoped<DataContext>(provider => provider.GetRequiredService<SqliteDbContext>());
        return services;
    }

    private static IServiceCollection ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var database = configuration.GetValue<string>("BOARDBARD_POSTGRES_DATABASE");
        var userName = configuration.GetValue<string>("BOARDBARD_POSTGRES_USERNAME");
        var password = configuration.GetValue<string>("BOARDBARD_POSTGRES_PASSWORD");
        var port = configuration.GetValue<int?>("BOARDBARD_POSTGRES_PORT");
        ArgumentExceptionExtended.ThrowIfAnyNullOrWhiteSpace(database, userName, password);
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseNpgsql(builder =>
            {
                builder.ConfigureDataSource(datasource =>
                {
                    datasource.ConnectionStringBuilder.Database = database;
                    datasource.ConnectionStringBuilder.Username = userName;
                    datasource.ConnectionStringBuilder.Password = password;
                    datasource.ConnectionStringBuilder.Port = port ?? 5432;
                });
            });
        });
        services.AddScoped<DataContext>(provider => provider.GetRequiredService<PostgresDbContext>());
        return services;
    }
}