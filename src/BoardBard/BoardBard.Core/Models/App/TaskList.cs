using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskLists", Schema = "Core")]
public class TaskList
{
    [Key] public int TaskListId { get; set; }
    [MaxLength(50)] public string ListName { get; set; }
    [MaxLength(20)] public string? Color { get; set; }
    public int Sequence { get; set; }
    public bool Collapsed { get; set; }
    public ICollection<TaskItem> Tasks { get; set; } = [];
}