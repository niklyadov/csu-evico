namespace Evico.Entity;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; }
    public double LocationLongitude { get; set; }
    public ProfileRecord Owner { get; set; }
    public String Name { get; set; }
    public Uri PhotoUri { get; set; }
    public List<Uri> PhotoUris { get; set; }
    public List<ReviewRecord> Reviews { get; set; }
    public List<PlaceCategoryRecord> Categories { get; set; }
}