using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Place;

public class AddPlaceInputModel
{
    [Required] public double LocationLatitude { get; set; } = default!;

    [Required] public double LocationLongitude { get; set; } = default!;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1024)] public string Description { get; set; } = string.Empty;

    public long? ParentId { get; set; }
}