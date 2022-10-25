namespace Evico.Entity;

public record EventCategoryRecord : CategoryRecord
{
    public virtual List<EventRecord> Events { get; set; } = new();
}