using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskBoards", Schema = "Core")]
public class TaskBoard
{
    public int TaskBoardId { get; set; }
    [MaxLength(50)] public string BoardName { get; set; }
    public bool Starred { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<TaskCard> TaskCards { get; set; } = [];
}