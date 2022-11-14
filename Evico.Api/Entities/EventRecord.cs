using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record EventRecord : EntityRecord
{
    public long OwnerId { get; set; }
    public ProfileRecord Owner { get; set; } = default!;
    public long PlaceId { get; set; }
    public PlaceRecord Place { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? Start { get; set; } = null;
    public DateTime? End { get; set; } = null;
    public virtual List<EventPhotoRecord> Photos { get; set; } = new();
    public virtual List<ProfileRecord> Organizers { get; set; } = new();
    public virtual List<ProfileRecord> Participants { get; set; } = new();

    [JsonIgnore] public virtual List<EventReviewRecord> Reviews { get; set; } = new();

    public virtual List<EventCategoryRecord> Categories { get; set; } = new();
}