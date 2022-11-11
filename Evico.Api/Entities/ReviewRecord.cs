using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Api.Entities;

[NotMapped]
public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public long AuthorId { get; set; }
    public String? Comment { get; set; } = String.Empty;
    public Rate Rate { get; set; }
    public virtual List<PhotoRecord> Photos { get; set; } = new();
}