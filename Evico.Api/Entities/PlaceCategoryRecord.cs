using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

public record PlaceCategoryRecord : CategoryRecord
{
    [JsonIgnore]
    public virtual List<PlaceRecord> Places { get; set; } = new();
}