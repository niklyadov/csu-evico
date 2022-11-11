using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record PhofilePhotoRecord : PhotoRecord
{
    public long ProfileId { get; set; }
    [JsonIgnore]
    public ProfileRecord Profile { get; set; }
}