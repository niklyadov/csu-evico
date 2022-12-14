using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class GetPlaceReviewByIdUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public GetPlaceReviewByIdUseCase(PlaceService placeService, PlaceReviewService placeReviewService,
        AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<PlaceReviewRecord>> GetByIdAsync(long placeId, long reviewId,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var placeReviewWithIdResult = await _placeReviewService.GetByIdAsync(reviewId);
        if (placeReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeReviewWithIdResult.GetReport());
        var placeReview = placeReviewWithIdResult.Value;

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

        var canViewResult = _placeReviewService.CanView(place, placeReview, currentUser);
        if (canViewResult.IsFailed)
            return new ObjectResult(canViewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        return new OkObjectResult(placeReview);
    }
}