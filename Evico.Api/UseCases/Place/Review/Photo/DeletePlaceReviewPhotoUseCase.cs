using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review.Photo;

public class DeletePlaceReviewPhotoUseCase
{
    private readonly PlaceReviewPhotoService _photoService;
    private readonly PlaceService _placeService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly AuthService _authService;

    public DeletePlaceReviewPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<PlaceReviewPhotoService>();
        _placeService = services.GetRequiredService<PlaceService>();
        _placeReviewService = services.GetRequiredService<PlaceReviewService>();
        _authService = services.GetRequiredService<AuthService>();
    }
    
    public async Task<ActionResult<PhotoRecord>> DeleteAsync(long placeId, long photoId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;
        
        var placeWithIdResult = await _placeService.GetByIdAsync(placeId);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var place = placeWithIdResult.Value;

        var canViewPlaceResult = _placeService.CanView(place, currentUser);
        if (canViewPlaceResult.IsFailed)
            return new ObjectResult(canViewPlaceResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var getPlaceReviewWithIdResult = await _placeReviewService.GetByIdAsync(placeId);
        if (getPlaceReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(getPlaceReviewWithIdResult.GetReport());
        var review = getPlaceReviewWithIdResult.Value;
        
        var canUpdatePlaceReviewResult = _placeReviewService.CanUpdate(place, review, currentUser);
        if (canUpdatePlaceReviewResult.IsFailed)
            return new ObjectResult(canUpdatePlaceReviewResult.GetReport())
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