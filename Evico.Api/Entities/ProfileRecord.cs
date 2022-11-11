using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record ProfileRecord : User
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    public PhofilePhotoRecord? Photo { get; set; } = null;
    public long? VkUserId { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public virtual List<EventRecord> OwnEvents { get; set; } = new();

    [JsonIgnore] public virtual List<EventRecord> OrganizerEvents { get; set; } = new();

    [JsonIgnore] public virtual List<EventRecord> ParticipantEvents { get; set; } = new();

    [JsonIgnore] public virtual List<PlaceRecord> OwnPlaces { get; set; } = new();

    [JsonIgnore] public virtual List<EventReviewRecord> OwnEventReviews { get; set; } = new();

    [JsonIgnore] public virtual List<PlaceReviewRecord> OwnPlaceReviews { get; set; } = new();
}