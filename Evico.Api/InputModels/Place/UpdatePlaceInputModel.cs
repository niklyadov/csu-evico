using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Place;

public class UpdatePlaceInputModel
{
    public long Id { get; set; }
    public double? LocationLatitude { get; set; }
    public double? LocationLongitude { get; set; }
    
    [StringLength(100)]
    public string? Name { get; set; }
    
    [StringLength(1024)]
    public string? Description { get; set; }
}