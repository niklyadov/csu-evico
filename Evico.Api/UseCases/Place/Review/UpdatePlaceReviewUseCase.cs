using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class UpdatePlaceReviewUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public UpdatePlaceReviewUseCase(PlaceService placeService, PlaceReviewService placeReviewService,
        AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceReviewRecord>>> UpdateAsync(long placeId,
        UpdatePlaceInputModel inputModel, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeReviewWithIdResult = await _placeReviewService.GetByIdAsync(inputModel.Id);
        if (placeReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeReviewWithIdResult.GetReport());
        var placeReview = placeReviewWithIdResult.Value;

        var canUpdateResult = _placeReviewService.CanUpdate(placeReview, currentUser);
        if (canUpdateResult.IsFailed)
            return new ObjectResult(canUpdateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var updateResult = await _placeReviewService.DeleteAsync(placeReview);
        if (updateResult.IsFailed)
            return new BadRequestObjectResult(updateResult.GetReport());

        return new OkObjectResult(updateResult.Value);
    }
}