using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record PlaceReviewPhotoRecord : PhotoRecord
{
    [JsonIgnore] public PlaceReviewRecord Review { get; set; } = default!;
    public long ReviewId { get; set; }
}