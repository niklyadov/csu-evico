using System.ComponentModel.DataAnnotations;

namespace Evico.Api.InputModels.Photo;

public class PhotoUploadInputModel
{
    [StringLength(1024)]
    public String? Comment { get; set; }
    public IFormFile Image { get; set; }
}