using System.ComponentModel.DataAnnotations;
using Evico.Api.Enums;

namespace Evico.Api.InputModels.Place;

public class PlaceSearchInputModel : BaseSearchInputModel
{
    [RegularExpression("[1-9]+(,?[1-9]+)+", 
        ErrorMessage = "Invalid patten. Valid pattern: '1,2,99'.")]
    [StringLength(64)]
    public String? InCategories { get; set; }
    [RegularExpression("[1-9]+(,?[1-9]+)+", 
        ErrorMessage = "Invalid patten. Valid pattern: '1,2,99'.")]
    [StringLength(64)]
    public String? NotInCategories { get; set; }
    [Range(1, Int32.MaxValue)]
    public long? OwnerId { get; set; }
    public PlaceSearchSortType SortBy { get; set; }
        = PlaceSearchSortType.Id;
}