using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskActivities", Schema = "Core")]
public class TaskActivity
{
    [Key]
    public int TaskActivityId { get; set; }
    public int TaskId { get; set; }
    public string Detail { get; set; }
    public DateTime CreatedAt { get; set; }
}