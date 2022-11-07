using System.Text.Json.Serialization;

namespace Evico.Api.Entity;

public record EventReviewRecord : ReviewRecord
{
    [JsonIgnore] public EventRecord Event { get; set; } = default!;

    public long EventId { get; set; }
}