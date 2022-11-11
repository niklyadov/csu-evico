using System.ComponentModel.DataAnnotations;
using Evico.Api.Attributes;

namespace Evico.Api.InputModels.Photo;

public class PhotoUploadInputModel
{
    [StringLength(1024)]
    public String? Comment { get; set; }

    [Required(ErrorMessage = "Please select a file.")]
    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)] // 5MB
    [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
    [AllowedContentType(new[] {"image/jpeg", "image/pjpeg", "image/png"})]
    public IFormFile Image { get; set; } = default!;
}