using BoardBard.Core.Contexts;
using BoardBard.Core.Models.App;

namespace BoardBard.Core.Extensions;

public static class DbContextExtensions
{
    public static IQueryable<TaskItem> NotCompletedItems(this DataContext context)
    {
        return context.TaskItems.Where(x => !x.IsCompleted);
    }
}