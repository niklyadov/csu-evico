namespace Evico.Entity;

public record EventRecord : EntityRecord
{
    public ProfileRecord Owner { get; set; }
    public ProfileRecord Organizer { get; set; }
    public PlaceRecord PlaceRecord { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ICollection<long> ParticipantProfileIds { get; set; }
}