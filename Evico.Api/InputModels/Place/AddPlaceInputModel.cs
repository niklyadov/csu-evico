using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Place;

public class AddPlaceInputModel
{
    [Required]
    public double LocationLatitude { get; set; } = default!;
    
    [Required]
    public double LocationLongitude { get; set; } = default!;
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public String Name { get; set; } = String.Empty;
    
    [StringLength(1024)]
    public String Description { get; set; } = String.Empty;
}