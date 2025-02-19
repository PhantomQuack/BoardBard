using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardBard.Core.Models.App;

[Table("LabelTypes", Schema = "Core")]
public class LabelType
{
    [Key] public int LabelTypeId { get; set; }
    [MaxLength(50)] public string? Name { get; set; }
    public string ColorHex { get; set; }
}