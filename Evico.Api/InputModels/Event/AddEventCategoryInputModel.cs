using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class AddEventCategoryInputModel
{
    [StringLength(255, MinimumLength = 1)] public string Name { get; set; } = string.Empty;

    [StringLength(1024)] public string Description { get; set; } = string.Empty;

    public long? ParentCategoryId { get; set; }
}