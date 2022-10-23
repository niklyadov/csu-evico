namespace Evico.Entity;

public record ProfileRecord : EntityRecord
{
    public string Name { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}