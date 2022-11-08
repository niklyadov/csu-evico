using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class UpdateEventCategoryInputModel
{
    public long Id { get; set; }
    [StringLength(255, MinimumLength = 1)]
    public String? Name { get; set; }
    [StringLength(1024)]
    public String? Description { get; set; }
    public long? ParentCategoryId { get; set; }
}