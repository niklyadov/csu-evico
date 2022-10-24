namespace Evico.Entity;

public record EventReviewRecord() : ReviewRecord
{
    public EventRecord Event { get; set; }
    public long EventId { get; set; }
}