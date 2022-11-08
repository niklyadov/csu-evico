using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Api.Entities;

[NotMapped]
public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public long AuthorId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public Rate Rate { get; set; }
    public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
}