using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Api.Entity;

[NotMapped]
public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public long AuthorId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Rate { get; set; }
    public virtual List<ExternalPhoto> Photos { get; set; } = new();
}