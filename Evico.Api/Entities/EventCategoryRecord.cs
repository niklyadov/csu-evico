namespace Evico.Api.Entities;

public record EventCategoryRecord : CategoryRecord
{
    public virtual List<EventRecord> Events { get; set; } = new();
}