using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("Tasks", Schema = "Core")]
public class TaskItem
{
    [Key] public int TaskId { get; set; }

    [Required, ForeignKey(nameof(TaskCard))]
    public int TaskCardId { get; set; }

    public TaskCard TaskCard { get; set; }
    [MaxLength(50)] public string Name { get; set; }
    [MaxLength(40000)] public string Description { get; set; }
    public int Sequence { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public virtual ICollection<TaskLabelLink> LabelLinks { get; set; } = [];
    public virtual ICollection<TaskActivity> Activities { get; set; } = [];
}