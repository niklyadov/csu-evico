namespace Evico.Api.InputModels;

public class EventInputModel
{
    public long? Id { get; set; }
    public long PlaceId { get; set; }
    public DateTime? Start { get; set; } = null;

    public DateTime? End { get; set; } = null;
    // TODO: add photo
    //public long PhotoId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}