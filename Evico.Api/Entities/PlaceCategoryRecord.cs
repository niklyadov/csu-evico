namespace Evico.Api.Entities;

public record PlaceCategoryRecord : CategoryRecord
{
    public virtual List<PlaceRecord> Places { get; set; } = new();
}