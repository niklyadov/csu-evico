namespace Evico.Api.Entity;

public record PlaceReviewRecord : ReviewRecord
{
    public long PlaceId { get; set; }
    public PlaceRecord Place { get; set; } = default!;
}