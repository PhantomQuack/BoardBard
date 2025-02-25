using Microsoft.EntityFrameworkCore;

namespace BoardBard.Core.Contexts;

public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DataContext(options);