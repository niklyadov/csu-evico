using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record ProfilePhotoRecord : PhotoRecord
{
    public long ProfileId { get; set; }
    [JsonIgnore]
    public ProfileRecord Profile { get; set; }
}