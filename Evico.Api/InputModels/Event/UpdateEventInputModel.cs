using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class UpdateEventInputModel
{
    public long Id { get; set; }
    public long? PlaceId { get; set; }
    public DateTime? Start { get; set; } = null;

    public DateTime? End { get; set; } = null;
    // TODO: add photo
    //public long PhotoId { get; set; }

    [StringLength(255, MinimumLength = 1)] public string? Name { get; set; }

    [StringLength(1024)] public string? Description { get; set; }
}