using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class AddEventInputModel
{
    public long PlaceId { get; set; }
    public DateTime? Start { get; set; } = null;
    public DateTime? End { get; set; } = null;

    [StringLength(255, MinimumLength = 1)] 
    public String Name { get; set; } = String.Empty;

    [StringLength(1024)] 
    public String Description { get; set; } = String.Empty;
    
    public List<long>? CategoryIds { get; set; }
}