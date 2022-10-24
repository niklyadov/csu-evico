namespace Evico.Entity;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; } = default!;
    public double LocationLongitude { get; set; } = default!;
    //public ProfileRecord Owner { get; set; } = default!;
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public ExternalPhoto? Photo { get; set; } = null;
    public virtual List<ExternalPhoto> Photos { get; set; } = new();
    public virtual List<PlaceReviewRecord> Reviews { get; set; } = new();
    public virtual List<PlaceCategoryRecord> Categories { get; set; } = new();
}