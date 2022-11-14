using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Photo;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Photo;

public class AddEventPhotoUseCase
{
    private readonly AuthService _authService;
    private readonly EventService _eventService;
    private readonly FileService _fileService;
    private readonly EventPhotoService _photoService;

    public AddEventPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<EventPhotoService>();
        _fileService = services.GetRequiredService<FileService>();
        _eventService = services.GetRequiredService<EventService>();
        _authService = services.GetRequiredService<AuthService>();
    }

    public async Task<ActionResult<PhotoRecord>> AddAsync(PhotoUploadInputModel inputModel, long eventId,
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

        var canUploadPhotoResult = _photoService.CanUpload(currentUser);
        if (canUploadPhotoResult.IsFailed)
            return new BadRequestObjectResult(canUploadPhotoResult.GetReport());

        var internalOperationId = Guid.NewGuid();
        var minioBucket = MinioBucketNames.EventPhotos;

        var photoRecord = new EventPhotoRecord
        {
            MinioInternalId = internalOperationId,
            MinioBucket = minioBucket,
            Author = currentUser,
            Comment = inputModel.Comment,
            Event = eventRecord
        };

        var photoUploadFileResult = await _fileService.UploadFile(inputModel.Image, minioBucket,
            internalOperationId.ToString());
        if (photoUploadFileResult.IsFailed)
            return new BadRequestObjectResult(photoUploadFileResult.GetReport());

        var addPhotoResult = await _photoService.AddAsync(photoRecord);
        if (addPhotoResult.IsFailed)
            return new BadRequestObjectResult(addPhotoResult.GetReport());
        var addedPhoto = addPhotoResult.Value;

        eventRecord.Photos.Add(addedPhoto);

        var updateEventResult = await _eventService.UpdateAsync(eventRecord);
        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return addedPhoto;
    }
}