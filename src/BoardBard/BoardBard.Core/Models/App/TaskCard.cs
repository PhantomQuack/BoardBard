using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BoardBard.Core.Models.App;

[Table("TaskCards", Schema = "Core")]
public class TaskCard
{
    [Key] public int TaskCardId { get; set; }

    [Required, ForeignKey(nameof(TaskBoard))]
    public int TaskBoardId { get; set; }

    public TaskBoard TaskBoard { get; set; }
    [MaxLength(50)] public string CardName { get; set; }
    [MaxLength(20)] public string? Color { get; set; }
    public int Sequence { get; set; }
    public bool Collapsed { get; set; }
    public virtual ICollection<TaskItem> TaskItems { get; set; } = [];
}