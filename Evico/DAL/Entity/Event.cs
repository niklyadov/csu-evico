namespace Evico.DAL.Entity;

public record Event : Entity
{
    public Profile Owner { get; set; }
    public Profile Organizer { get; set; }
    public Place Place { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ICollection<long> ParticipantProfileIds { get; set; }
}