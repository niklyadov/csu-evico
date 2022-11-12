using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Photo;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review.Photo;

public class AddPlaceReviewPhotoUseCase
{
    private readonly AuthService _authService;
    private readonly FileService _fileService;
    private readonly PlaceReviewPhotoService _photoService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public AddPlaceReviewPhotoUseCase(IServiceProvider services)
    {
        _photoService = services.GetRequiredService<PlaceReviewPhotoService>();
        _fileService = services.GetRequiredService<FileService>();
        _placeService = services.GetRequiredService<PlaceService>();
        _placeReviewService = services.GetRequiredService<PlaceReviewService>();
        _authService = services.GetRequiredService<AuthService>();
    }

    public async Task<ActionResult<PhotoRecord>> AddAsync(PhotoUploadInputModel inputModel, long placeId, long reviewId,
        ClaimsPrincipal claimsPrincipal)
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

        var getReviewWithIdResult = await _placeReviewService.GetByIdAsync(reviewId);
        if (getReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(getReviewWithIdResult.GetReport());
        var review = getReviewWithIdResult.Value;

        var canUpdateReviewResult = _placeReviewService.CanUpdate(place, review, currentUser);
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

        var photoRecord = new PlaceReviewPhotoRecord
        {
            MinioInternalId = internalOperationId,
            MinioBucket = minioBucket,
            Author = currentUser,
            Comment = inputModel.Comment,
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

        var updatePlaceResult = await _placeReviewService.UpdateAsync(review);
        if (updatePlaceResult.IsFailed)
            return new BadRequestObjectResult(updatePlaceResult.GetReport());

        return addedPhoto;
    }
}