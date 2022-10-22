namespace Evico.Entity;

public record EventRecord : EntityRecord
{
    public ProfileRecord Owner { get; set; }
    public ICollection<ProfileRecord> Organizers { get; set; }
    public PlaceRecord PlaceRecord { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ICollection<long> ParticipantProfileIds { get; set; }
    public List<ReviewRecord> Reviews { get; set; }
}