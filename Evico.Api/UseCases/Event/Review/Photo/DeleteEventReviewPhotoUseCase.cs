using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review.Photo;

public class DeleteEventReviewPhotoUseCase
{
    private readonly EventReviewPhotoService _photoService;
    private readonly FileService _fileService;
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public DeleteEventReviewPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<EventReviewPhotoService>();
        _fileService = services.GetRequiredService<FileService>();
        _eventService = services.GetRequiredService<EventService>();
        _eventReviewService = services.GetRequiredService<EventReviewService>();
        _authService = services.GetRequiredService<AuthService>();
    }
    
    public async Task<ActionResult<PhotoRecord>> DeleteAsync(long eventId, long photoId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;
        
        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(eventWithIdResult.GetReport());
        var eventRecord = eventWithIdResult.Value;

        var canViewEventResult = _eventService.CanView(eventRecord, currentUser);
        if (canViewEventResult.IsFailed)
            return new ObjectResult(canViewEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var getEventReviewWithIdResult = await _eventReviewService.GetByIdAsync(eventId);
        if (getEventReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(getEventReviewWithIdResult.GetReport());
        var review = getEventReviewWithIdResult.Value;
        
        var canUpdateEventReviewResult = _eventReviewService.CanUpdate(eventRecord, review, currentUser);
        if (canUpdateEventReviewResult.IsFailed)
            return new ObjectResult(canUpdateEventReviewResult.GetReport())
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