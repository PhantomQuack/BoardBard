using BoardBard.Core.Models.App;
using Microsoft.EntityFrameworkCore;

namespace BoardBard.Core.Contexts;

public class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : DbContext(options)
{
    public virtual DbSet<LabelType> LabelTypes { get; set; }
    public virtual DbSet<TaskActivity> TaskActivities { get; set; }
    public virtual DbSet<TaskBoard> TaskBoards { get; set; }
    public virtual DbSet<TaskItem> TaskItems { get; set; }
    public virtual DbSet<TaskLabelLink> TaskLabelLinks { get; set; }
    public virtual DbSet<TaskList> TaskLists { get; set; }
}