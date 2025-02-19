using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("TaskLabelLinks", Schema = "Core")]
public class TaskLabelLink
{
    [Key] public int TaskLabelLinkId { get; set; }

    [Required, ForeignKey(nameof(TaskItem))]
    public int TaskId { get; set; }

    public TaskItem TaskItem { get; set; }

    [Required, ForeignKey(nameof(LabelType))]
    public int LabelTypeId { get; set; }

    public LabelType LabelType { get; set; }
}