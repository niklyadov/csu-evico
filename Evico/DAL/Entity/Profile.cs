namespace Evico.DAL.Entity;

public record Profile : Entity
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}