using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Event;

public class AddEventInputModel
{
    public long? Id { get; set; }
    public long PlaceId { get; set; }
    public DateTime? Start { get; set; } = null;

    public DateTime? End { get; set; } = null;
    // TODO: add photo
    //public long PhotoId { get; set; }

    [MinLength(1)] [MaxLength(255)] public string Name { get; set; } = string.Empty;

    [MinLength(1)] [MaxLength(1024)] public string Description { get; set; } = string.Empty;
}