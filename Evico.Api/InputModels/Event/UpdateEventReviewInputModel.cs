using System.ComponentModel.DataAnnotations;
using Evico.Api.Entity;

namespace Evico.Api.InputModels.Event;

public class UpdateEventReviewInputModel
{
    public long Id { get; set; }
    [MinLength(8)]
    [MaxLength(1024)]
    public String Comment { get; set; } = String.Empty;
    public Rate Rate { get; set; }
    [MaxLength(16)]
    public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
}