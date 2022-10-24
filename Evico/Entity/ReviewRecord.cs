using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Entity;

[NotMapped]
public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public String Comment { get; set; } = String.Empty;
    public virtual List<ExternalPhoto> Photos { get; set; } = new();
}