using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Photo;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review.Photo;

public class AddEventReviewPhotoUseCase
{
    private readonly EventReviewPhotoService _photoService;
    private readonly FileService _fileService;
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public AddEventReviewPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<EventReviewPhotoService>();
        _fileService = services.GetRequiredService<FileService>();
        _eventService = services.GetRequiredService<EventService>();
        _eventReviewService = services.GetRequiredService<EventReviewService>();
        _authService = services.GetRequiredService<AuthService>();
    }
    
    public async Task<ActionResult<PhotoRecord>> AddAsync(PhotoUploadInputModel inputModel, long eventId, long reviewId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var getByIdResult = await _eventService.GetByIdAsync(eventId);
        if (getByIdResult.IsFailed)
            return new BadRequestObjectResult(getByIdResult.GetReport());
        var eventRecord = getByIdResult.Value;

        var canViewEventResult = _eventService.CanView(eventRecord, currentUser);
        if (canViewEventResult.IsFailed)
            return new ObjectResult(canViewEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var getReviewWithIdResult = await _eventReviewService.GetByIdAsync(reviewId);
        if (getReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(getReviewWithIdResult.GetReport());
        var review = getReviewWithIdResult.Value;
        
        var canUpdateReviewResult = _eventReviewService.CanUpdate(eventRecord, review, currentUser);
        if (canUpdateReviewResult.IsFailed)
            return new ObjectResult(canUpdateReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var canUploadPhotoResult = _photoService.CanUpload(currentUser);
        if (canUploadPhotoResult.IsFailed)
            return new BadRequestObjectResult(canUploadPhotoResult.GetReport());

        var internalOperationId = Guid.NewGuid();
        var minioBucket = MinioBucketNames.ReviewPhotos;

        var photoRecord = new EventReviewPhotoRecord
        {
            MinioInternalId = internalOperationId,
            MinioBucket = minioBucket,
            Author = currentUser,
            Comment = inputModel.Comment,
            Uri = new Uri("/Photo/"),
            Review = review
        };
        
        var photoUploadFileResult = await _fileService.UploadFile(inputModel.Image, minioBucket, 
            internalOperationId.ToString());
        if (photoUploadFileResult.IsFailed)
            return new BadRequestObjectResult(photoUploadFileResult.GetReport());

        var addPhotoResult = await _photoService.AddAsync(photoRecord);
        if (addPhotoResult.IsFailed)
            return new BadRequestObjectResult(addPhotoResult.GetReport());
        var addedPhoto = addPhotoResult.Value;
        
        review.Photos.Add(addedPhoto);
        
        var updateEventResult = await _eventReviewService.Update(review);
        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());
        
        return addedPhoto;
    }
}