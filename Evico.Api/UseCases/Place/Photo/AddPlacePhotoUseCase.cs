using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Photo;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Photo;

public class AddPlacePhotoUseCase
{
    private readonly PlacePhotoService _photoService;
    private readonly FileService _fileService;
    private readonly PlaceService _placeService;
    private readonly AuthService _authService;

    public AddPlacePhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<PlacePhotoService>();
        _fileService = services.GetRequiredService<FileService>();
        _placeService = services.GetRequiredService<PlaceService>();
        _authService = services.GetRequiredService<AuthService>();
    }
    
    public async Task<ActionResult<PhotoRecord>> AddAsync(PhotoUploadInputModel inputModel, long placeId, 
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;
        
        var getPlaceWithIdResult = await _placeService.GetByIdAsync(placeId);
        if (getPlaceWithIdResult.IsFailed)
            return new BadRequestObjectResult(getPlaceWithIdResult.GetReport());
        var place = getPlaceWithIdResult.Value;
        
        var canUpdatePlaceResult = _placeService.CanUpdate(place, currentUser);
        if (canUpdatePlaceResult.IsFailed)
            return new ObjectResult(canUpdatePlaceResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var canUploadPhotoResult = _photoService.CanUpload(currentUser);
        if (canUploadPhotoResult.IsFailed)
            return new BadRequestObjectResult(canUploadPhotoResult.GetReport());

        var internalOperationId = Guid.NewGuid();
        var minioBucket = MinioBucketNames.PlacePhotos;

        var photoRecord = new PlacePhotoRecord
        {
            MinioInternalId = internalOperationId,
            MinioBucket = minioBucket,
            Author = currentUser,
            Comment = inputModel.Comment,
            Place = place
        };
        
        var photoUploadFileResult = await _fileService.UploadFile(inputModel.Image, minioBucket, 
            internalOperationId.ToString());
        if (photoUploadFileResult.IsFailed)
            return new BadRequestObjectResult(photoUploadFileResult.GetReport());

        var addPhotoResult = await _photoService.AddAsync(photoRecord);
        if (addPhotoResult.IsFailed)
            return new BadRequestObjectResult(addPhotoResult.GetReport());
        var addedPhoto = addPhotoResult.Value;
        
        place.Photos.Add(addedPhoto);
        
        var updatePlaceResult = await _placeService.UpdateAsync(place);
        if (updatePlaceResult.IsFailed)
            return new BadRequestObjectResult(updatePlaceResult.GetReport());
        
        return addedPhoto;
    }
}