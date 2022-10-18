namespace Evico.DAL.Entity;

public record Place : Entity
{
    public Coordinates Location { get; set; }
    public Profile Owner { get; set; }
    public String Name { get; set; }
}