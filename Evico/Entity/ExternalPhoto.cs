namespace Evico.Entity;

public record ExternalPhoto : EntityRecord
{
    public Uri Uri { get; set; } = default!;
}