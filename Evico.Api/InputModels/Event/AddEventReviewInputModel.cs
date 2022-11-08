using System.ComponentModel.DataAnnotations;
using Evico.Api.Entities;

namespace Evico.Api.InputModels.Event;

public class AddEventReviewInputModel
{
    [StringLength(1024, MinimumLength = 8)]
    public string Comment { get; set; } = string.Empty;

    public Rate Rate { get; set; }

    [MaxLength(16)] public virtual List<ExternalPhotoRecord> Photos { get; set; } = new();
}