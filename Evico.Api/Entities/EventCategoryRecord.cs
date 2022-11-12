using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record EventCategoryRecord : CategoryRecord
{
    [JsonIgnore] public virtual List<EventRecord> Events { get; set; } = new();
}