namespace Evico.Entity;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; } = default!;

    public double LocationLongitude { get; set; } = default!;

    //public ProfileRecord Owner { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ExternalPhoto? Photo { get; set; } = null;
    public virtual List<ExternalPhoto> Photos { get; set; } = new();
    public virtual List<PlaceReviewRecord> Reviews { get; set; } = new();
    public virtual List<PlaceCategoryRecord> Categories { get; set; } = new();
}