namespace Evico.Api.Entities;

public record PhotoRecord : EntityRecord
{
    public long? AuthorId { get; set; }
    public ProfileRecord? Author { get; set; }
    public String Comment { get; set; } = String.Empty;
    public Uri Uri { get; set; } = default!;
}