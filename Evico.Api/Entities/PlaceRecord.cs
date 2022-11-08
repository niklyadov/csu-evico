using Newtonsoft.Json;

namespace Evico.Api.Entities;

public record PlaceRecord : EntityRecord
{
    public double LocationLatitude { get; set; }

    public double LocationLongitude { get; set; }

    public long OwnerId { get; set; }
    public ProfileRecord Owner { get; set; } = default!;
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public ExternalPhotoRecord? Photo { get; set; } = null;
    public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
    [JsonIgnore]
    public virtual List<PlaceReviewRecord> Reviews { get; set; } = new();
    public virtual List<PlaceCategoryRecord> Categories { get; set; } = new();
    
    public long? ParentId { get; set; }
    public PlaceRecord? Parent { get; set; }
}