using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class AddEventCategoryInputModel
{
    [StringLength(255, MinimumLength = 1)] 
    public String Name { get; set; } = String.Empty;
    [StringLength(1024)]
    public String Description { get; set; } = String.Empty;
    public long? ParentCategoryId { get; set; }
}