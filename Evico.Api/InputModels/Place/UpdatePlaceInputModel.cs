namespace Evico.Api.InputModels.Place;

public class UpdatePlaceInputModel
{
    public long Id { get; set; }
    public double? LocationLatitude { get; set; }
    public double? LocationLongitude { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}