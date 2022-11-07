using System.ComponentModel.DataAnnotations;
using Evico.Api.Entity;

namespace Evico.Api.InputModels.Place;

public class UpdatePlaceReviewInputModel
{
    public long Id { get; set; }
    
    [StringLength(1024, MinimumLength = 8)]
    public String? Comment { get; set; }
    public Rate? Rate { get; set; }
    [MaxLength(16)]
    public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
}