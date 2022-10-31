namespace Evico.Api.Entity;

public record EventReviewRecord : ReviewRecord
{
    public EventRecord Event { get; set; } = default!;
    public long EventId { get; set; }
}