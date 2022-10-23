namespace Evico.Entity;

public record EventRecord : EntityRecord
{
    public ProfileRecord Owner { get; set; } = default!;
    public List<ProfileRecord> Organizers { get; set; } = new();
    public PlaceRecord PlaceRecord { get; set; } = default!;
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public List<long> ParticipantProfileIds { get; set; } = new();
    public List<ReviewRecord> Reviews { get; set; } = new();
    public List<EventCategoryRecord> Categories { get; set; } = new();
}