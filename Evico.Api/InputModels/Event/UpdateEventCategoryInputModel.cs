using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class UpdateEventCategoryInputModel
{
    public long Id { get; set; }

    [StringLength(255, MinimumLength = 1)] public string? Name { get; set; }

    [StringLength(1024)] public string? Description { get; set; }

    public long? ParentCategoryId { get; set; }
}