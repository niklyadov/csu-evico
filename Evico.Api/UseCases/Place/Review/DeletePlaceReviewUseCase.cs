using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class DeletePlaceReviewUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public DeletePlaceReviewUseCase(PlaceService placeService, PlaceReviewService placeReviewService,
        AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceReviewRecord>>> DeleteAsync(long placeId, long reviewId,
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

        var placeReviewWithIdResult = await _placeReviewService.GetByIdAsync(reviewId);
        if (placeReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeReviewWithIdResult.GetReport());
        var placeReview = placeReviewWithIdResult.Value;

        var canDeleteResult = _placeReviewService.CanDelete(place, placeReview, currentUser);
        if (canDeleteResult.IsFailed)
            return new ObjectResult(canDeleteResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var deleteResult = await _placeReviewService.DeleteAsync(placeReview);
        if (deleteResult.IsFailed)
            return new BadRequestObjectResult(deleteResult.GetReport());

        return new OkObjectResult(deleteResult.Value);
    }
}