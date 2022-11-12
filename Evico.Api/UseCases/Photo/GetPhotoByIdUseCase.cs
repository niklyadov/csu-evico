using System.Security.Claims;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Photo;

public class GetPhotoByIdUseCase
{
    private readonly PhotoService _photoService;
    private readonly FileService _fileService;
    private readonly AuthService _authService;

    public GetPhotoByIdUseCase(PhotoService photoService, FileService fileService, AuthService authService)
    {
        _photoService = photoService;
        _fileService = fileService;
        _authService = authService;
    }
    
    public async Task<IActionResult> GetByIdAsync(long photoId, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        //if (currentUserResult.IsFailed)
        //    return new UnauthorizedObjectResult(currentUserResult.GetReport());
        
        var getPhotoResult = await _photoService.GetByIdAsync(photoId);
        if (getPhotoResult.IsFailed)
            return new BadRequestObjectResult(getPhotoResult.GetReport());
        var photo = getPhotoResult.Value;

        var getPhotoFileResult = await _fileService.DownloadFile(photo.MinioBucket, photo.MinioInternalId.ToString());
        if (getPhotoFileResult.IsFailed)
            return new BadRequestObjectResult(getPhotoFileResult.GetReport());

        return getPhotoFileResult.Value;
    }
}