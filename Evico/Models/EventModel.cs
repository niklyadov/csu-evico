namespace Evico.Models;

public class EventModel 
{
    public long? Id { get; set; }
    public long PlaceId { get; set; }
    public DateTime? Start { get; set; } = null;
    public DateTime? End { get; set; } = null;
    // TODO: add photo
    //public long PhotoId { get; set; }
    
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
}