using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class DeletePlaceReviewUseCase
{
    private readonly PlaceService _placeService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly AuthService _authService;

    public DeletePlaceReviewUseCase(PlaceService placeService, PlaceReviewService placeReviewService, AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }
    
    public async Task<ActionResult<List<PlaceReviewRecord>>> DeleteAsync(long placeId, long reviewId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeReviewWithIdResult = await _placeReviewService.GetByIdAsync(reviewId);
        if (placeReviewWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeReviewWithIdResult.GetReport());
        var placeReview = placeReviewWithIdResult.Value;
        
        var canDeleteResult = _placeReviewService.CanDelete(placeReview, currentUser);
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