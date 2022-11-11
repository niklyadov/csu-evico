using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record EventReviewPhotoRecord : PhotoRecord
{
    [JsonIgnore]
    public EventReviewRecord Review { get; set; }
    public long ReviewId { get; set; }
};