using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskBoards", Schema = "Core")]
public class TaskBoard
{
    [Key]
    public int TaskBoardId { get; set; }
    [MaxLength(50)]
    public string BoardName { get; set; }
    public ICollection<TaskItem> TaskItems { get; set; } = [];
}