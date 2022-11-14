using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record EventReviewRecord : ReviewRecord
{
    [JsonIgnore] public EventRecord Event { get; set; } = default!;

    public long EventId { get; set; }
}