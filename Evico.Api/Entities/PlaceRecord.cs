namespace Evico.Api.Entities;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; } = default!;

    public double LocationLongitude { get; set; } = default!;

    public long OwnerId { get; set; }
    public ProfileRecord Owner { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ExternalPhotoRecord? Photo { get; set; } = null;
    public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
    public virtual List<PlaceReviewRecord> Reviews { get; set; } = new();
    public virtual List<PlaceCategoryRecord> Categories { get; set; } = new();
}