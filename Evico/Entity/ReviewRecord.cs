namespace Evico.Entity;

public record ReviewRecord : EntityRecord
{
    public ProfileRecord Author { get; set; }
    public String Comment { get; set; }
}