namespace Evico.Entity;

public record PlaceRecord : EntityRecord
{
    public Coordinates Location { get; set; }
    public ProfileRecord Owner { get; set; }
    public String Name { get; set; }
    public Uri PhotoUri { get; set; }
    public List<Uri> PhotoUris { get; set; }
    public List<ReviewRecord> Reviews { get; set; }
}