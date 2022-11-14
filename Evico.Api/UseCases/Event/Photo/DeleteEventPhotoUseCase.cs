using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Photo;

public class DeleteEventPhotoUseCase
{
    private readonly AuthService _authService;
    private readonly EventService _eventService;
    private readonly EventPhotoService _photoService;

    public DeleteEventPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<EventPhotoService>();
        _eventService = services.GetRequiredService<EventService>();
        _authService = services.GetRequiredService<AuthService>();
    }

    public async Task<ActionResult<PhotoRecord>> DeleteAsync(long eventId, long photoId,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var getEventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (getEventWithIdResult.IsFailed)
            return new BadRequestObjectResult(getEventWithIdResult.GetReport());
        var eventRecord = getEventWithIdResult.Value;

        var canUpdateEventResult = _eventService.CanUpdate(eventRecord, currentUser);
        if (canUpdateEventResult.IsFailed)
            return new ObjectResult(canUpdateEventResult.GetReport())
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