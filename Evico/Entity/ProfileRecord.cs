namespace Evico.Entity;

public record ProfileRecord : EntityRecord
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}