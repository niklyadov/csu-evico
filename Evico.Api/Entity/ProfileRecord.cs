namespace Evico.Api.Entity;

public record ProfileRecord : EntityRecord
{
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ExternalPhoto? Photo { get; set; } = null;

    //public virtual List<EventRecord> OwnEvents { get; set; }
    public virtual List<EventRecord> OrganizerEvents { get; set; } = new();

    public virtual List<EventRecord> ParticipantEvents { get; set; } = new();
    //public virtual List<PlaceRecord> OwnPlaces { get; set; }
}