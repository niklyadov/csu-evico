namespace Evico.Entity;

public record EventRecord : EntityRecord
{
    //public ProfileRecord Owner { get; set; } = default!;
    public PlaceRecord Place { get; set; } = default!;
    public DateTime? Start { get; set; } = null;
    public DateTime? End { get; set; } = null;
    public ExternalPhoto? Photo { get; set; } = null;
    public virtual List<ProfileRecord> Organizers { get; set; } = new();
    public virtual List<ProfileRecord> Participants { get; set; } = new();
    public virtual List<ReviewRecord> Reviews { get; set; } = new();
    public virtual List<EventCategoryRecord> Categories { get; set; } = new();
}