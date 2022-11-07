namespace Evico.Api.InputModels.Place;

public class AddPlaceInputModel
{
    public double LocationLatitude { get; set; } = default!;
    public double LocationLongitude { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}