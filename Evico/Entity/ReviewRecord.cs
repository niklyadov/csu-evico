namespace Evico.Entity;

public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; } = default!;
    public String Comment { get; set; } = String.Empty;
}