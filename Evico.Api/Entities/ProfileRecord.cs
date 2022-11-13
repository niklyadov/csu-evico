using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record ProfileRecord : User
{
    public String Firstname { get; set; } = String.Empty;
    public String Lastname { get; set; } = String.Empty;

    public ProfilePhotoRecord? Photo { get; set; }
    public long? VkUserId { get; set; }
    public UserRoles Role { get; set; } = UserRoles.Default;
    public DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public virtual List<EventRecord> OwnEvents { get; set; } = new();

    [JsonIgnore] public virtual List<EventRecord> OrganizerEvents { get; set; } = new();

    [JsonIgnore] public virtual List<EventRecord> ParticipantEvents { get; set; } = new();

    [JsonIgnore] public virtual List<PlaceRecord> OwnPlaces { get; set; } = new();

    [JsonIgnore] public virtual List<EventReviewRecord> OwnEventReviews { get; set; } = new();

    [JsonIgnore] public virtual List<PlaceReviewRecord> OwnPlaceReviews { get; set; } = new();
}