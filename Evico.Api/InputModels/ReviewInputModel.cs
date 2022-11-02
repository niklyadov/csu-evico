namespace Evico.Api.InputModels;

public class ReviewInputModel
{
    public long EventId { get; set; }
    public long AuthorId { get; set; }
    public int Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
    // TODO: add photos
}