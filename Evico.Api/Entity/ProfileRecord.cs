namespace Evico.Api.Entity;

public record ProfileRecord : User
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    public ExternalPhoto? Photo { get; set; } = null;
    public long? VkUserId { get; set; }
    public DateTime? BirthDate { get; set; }

    public virtual List<EventRecord> OwnEvents { get; set; } = new();
    public virtual List<EventRecord> OrganizerEvents { get; set; } = new();
    public virtual List<EventRecord> ParticipantEvents { get; set; } = new();
    public virtual List<PlaceRecord> OwnPlaces { get; set; } = new();
    public virtual List<EventReviewRecord> OwnEventReviews { get; set; } = new();
    public virtual List<PlaceReviewRecord> OwnPlaceReviews { get; set; } = new();
}