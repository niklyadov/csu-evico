using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record EventPhotoRecord : PhotoRecord
{
    [JsonIgnore]
    public EventRecord Event { get; set; }
    public long EventId { get; set; }
}