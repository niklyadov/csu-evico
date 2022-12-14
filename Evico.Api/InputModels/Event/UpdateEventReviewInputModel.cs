using System.ComponentModel.DataAnnotations;
using Evico.Api.Entities;

namespace Evico.Api.InputModels.Event;

public class UpdateEventReviewInputModel
{
    public long Id { get; set; }

    [StringLength(1024, MinimumLength = 8)]
    public string? Comment { get; set; }

    public Rate? Rate { get; set; }
}