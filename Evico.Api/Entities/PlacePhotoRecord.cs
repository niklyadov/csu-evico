using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record PlacePhotoRecord : PhotoRecord
{
    [JsonIgnore] public PlaceRecord Place { get; set; } = default!;
    public long PlaceId { get; set; }
}