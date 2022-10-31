namespace Evico.Api.Entity;

public record ExternalPhoto : EntityRecord
{
    public Uri Uri { get; set; } = default!;
}