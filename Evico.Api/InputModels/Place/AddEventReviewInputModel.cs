using System.ComponentModel.DataAnnotations;
using Evico.Api.Entities;

namespace Evico.Api.InputModels.Place;

public class AddPlaceReviewInputModel
{
    [StringLength(1024, MinimumLength = 8)]
    public string? Comment { get; set; }

    public Rate Rate { get; set; }
}