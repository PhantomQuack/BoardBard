using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("Tasks", Schema = "Core")]
public abstract class TaskItem
{
    [Key] public int TaskId { get; set; }
    [MaxLength(50)] public string TaskName { get; set; }
    [MaxLength(40000)] public string TaskDescription { get; set; }
    public int Sequence { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public virtual ICollection<TaskLabelLink> LabelLinks { get; set; } = [];
    public virtual ICollection<TaskActivity> Activities { get; set; } = [];
}