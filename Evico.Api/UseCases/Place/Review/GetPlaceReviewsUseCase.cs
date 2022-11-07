using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class GetPlaceReviewsUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public GetPlaceReviewsUseCase(PlaceService placeService, PlaceReviewService placeReviewService,
        AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceReviewRecord>>> GetAllAsync(long placeId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var placeWithIdResult = await _placeService.GetByIdAsync(placeId);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var place = placeWithIdResult.Value;

        var canViewResult = _placeReviewService.CanViewAll(place, currentUser);
        if (canViewResult.IsFailed)
            return new ObjectResult(canViewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeReviewsResult = await _placeReviewService.GetAllAsync();
        if (placeReviewsResult.IsFailed)
            return new BadRequestObjectResult(placeReviewsResult.GetReport());

        var placeReview = placeReviewsResult.Value;


        return new OkObjectResult(placeReview);
    }
}