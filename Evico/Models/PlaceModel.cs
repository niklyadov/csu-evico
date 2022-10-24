namespace Evico.Models;

public class PlaceModel
{
    public long? Id { get; set; }
    public double LocationLatitude { get; set; } = default!;
    public double LocationLongitude { get; set; } = default!;
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
}