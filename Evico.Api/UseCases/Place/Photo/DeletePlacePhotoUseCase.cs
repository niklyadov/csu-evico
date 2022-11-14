using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Photo;

public class DeletePlacePhotoUseCase
{
    private readonly AuthService _authService;
    private readonly PlacePhotoService _photoService;
    private readonly PlaceService _placeService;

    public DeletePlacePhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<PlacePhotoService>();
        _placeService = services.GetRequiredService<PlaceService>();
        _authService = services.GetRequiredService<AuthService>();
    }

    public async Task<ActionResult<PhotoRecord>> DeleteAsync(long placeId, long photoId,
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

        var photoWithIdResult = await _photoService.GetByIdAsync(photoId);
        if (photoWithIdResult.IsFailed)
            return new BadRequestObjectResult(photoWithIdResult.GetReport());
        var photo = photoWithIdResult.Value;

        var canDeletePhotoResult = _photoService.CanDelete(photo, currentUser);
        if (canDeletePhotoResult.IsFailed)
            return new ObjectResult(canDeletePhotoResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var deletePhotoResult = await _photoService.DeleteAsync(photo);
        if (deletePhotoResult.IsFailed)
            return new BadRequestObjectResult(deletePhotoResult.GetReport());
        var deletedPhoto = deletePhotoResult.Value;

        return new OkObjectResult(deletedPhoto);
    }
}