namespace Evico.Entity;

public record ProfileRecord : EntityRecord
{
    public string Name { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public ExternalPhoto? Photo { get; set; } = null;
    public virtual List<EventRecord> OwnEvents { get; set; }
    public virtual List<EventRecord> OrganizerEvents { get; set; }
    public virtual List<EventRecord> ParticipantEvents { get; set; }
    public virtual List<PlaceRecord> OwnPlaces { get; set; }
}