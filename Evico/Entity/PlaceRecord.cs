namespace Evico.Entity;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; } = default!;
    public double LocationLongitude { get; set; } = default!;
    public ProfileRecord Owner { get; set; } = default!;
    public String Name { get; set; } = String.Empty;
    public Uri PhotoUri { get; set; } = default!;
    public List<Uri> PhotoUris { get; set; } = new();
    public List<ReviewRecord> Reviews { get; set; } = new();
    public List<PlaceCategoryRecord> Categories { get; set; } = new();
}