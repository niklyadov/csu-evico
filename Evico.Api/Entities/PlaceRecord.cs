using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; }

    public double LocationLongitude { get; set; }

    public long OwnerId { get; set; }
    public ProfileRecord Owner { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual List<PlacePhotoRecord> Photos { get; set; } = new();

    [JsonIgnore] public virtual List<PlaceReviewRecord> Reviews { get; set; } = new();

    public virtual List<PlaceCategoryRecord> Categories { get; set; } = new();

    public long? ParentId { get; set; }
    public PlaceRecord? Parent { get; set; }
}