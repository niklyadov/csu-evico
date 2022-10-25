namespace Evico.InputModels;

public class PlaceInputModel
{
    public long? Id { get; set; }
    public double LocationLatitude { get; set; } = default!;
    public double LocationLongitude { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}