namespace Evico.Api.Entity;

public record PlaceCategoryRecord : CategoryRecord
{
    public virtual List<PlaceRecord> Places { get; set; } = new();
}