using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskLabelLinks", Schema = "Core")]
public abstract class TaskLabelLink
{
    [Key]
    public int TaskLabelLinkId { get; set; }
    public int TaskId { get; set; }
    public int LabelTypeId { get; set; }
}