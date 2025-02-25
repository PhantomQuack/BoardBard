using Microsoft.EntityFrameworkCore;

namespace BoardBard.Core.Contexts;

public class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : DataContext(options);