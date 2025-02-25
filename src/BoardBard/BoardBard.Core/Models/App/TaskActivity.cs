using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskActivities", Schema = "Core")]
public class TaskActivity
{
    [Key] public int TaskActivityId { get; set; }

    [Required, ForeignKey(nameof(TaskItem))]
    public int TaskItemId { get; set; }

    public TaskItem TaskItem { get; set; }
    [MaxLength(4000)] public string Detail { get; set; }
    public DateTime CreatedAt { get; set; }
}