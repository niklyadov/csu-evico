using Evico.Api.UseCases.Photo;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class PhotoController : BaseController
{
    private readonly GetPhotoByIdUseCase _getPhotoByIdUseCase;

    public PhotoController(IServiceProvider services)
    {
        _getPhotoByIdUseCase = services.GetRequiredService<GetPhotoByIdUseCase>();
    }
    
    [HttpGet("{photoId}")]
    public async Task<IActionResult> GetById([FromRoute] long photoId)
    {
        return await _getPhotoByIdUseCase.GetByIdAsync(photoId, User);
    }
}