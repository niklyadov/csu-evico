using System.ComponentModel.DataAnnotations;
using Evico.Api.Enums;

namespace Evico.Api.InputModels.Event;

public class EventSearchFilters : BaseSearchInputModel
{
    [RegularExpression("[1-9]+(,[1-9]+)+", 
        ErrorMessage = "Invalid patten. Valid pattern: '1,2,99'.")]
    [StringLength(64)]
    public String? InCategories { get; set; }
    [RegularExpression("[1-9]+(,[1-9]+)+", 
        ErrorMessage = "Invalid patten. Valid pattern: '1,2,99'.")]
    [StringLength(64)]
    public String? NotInCategories { get; set; }
    [RegularExpression("[1-9]+(,[1-9]+)+", 
        ErrorMessage = "Invalid patten. Valid pattern: '1,2,99'.")]
    [StringLength(64)]
    public String? Organizers { get; set; }
    public EventSearchSortType SortBy { get; set; }
        = EventSearchSortType.Id;
    public long? PlaceId { get; set; }
    // todo: avg rate sort
    // public Rate? RateMoreThan { get; set; }
    public String? StartDate { get; set; }
    public String? EndDate { get; set; }
}