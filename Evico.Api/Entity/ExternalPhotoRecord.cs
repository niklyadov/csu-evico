namespace Evico.Api.Entity;

public record ExternalPhotoRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public long AuthorId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public Uri Uri { get; set; } = default!;
}