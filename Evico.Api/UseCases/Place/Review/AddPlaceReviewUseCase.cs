using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Review;

public class AddPlaceReviewUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceReviewService _placeReviewService;
    private readonly PlaceService _placeService;

    public AddPlaceReviewUseCase(PlaceService placeService, PlaceReviewService placeReviewService,
        AuthService authService)
    {
        _placeService = placeService;
        _placeReviewService = placeReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<PlaceReviewRecord>> AddAsync(long placeId, AddPlaceReviewInputModel inputModel,
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

        var canCreateResult = _placeReviewService.CanCreate(place, currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeReview = new PlaceReviewRecord
        {
            Comment = inputModel.Comment,
            Rate = inputModel.Rate,
            Author = currentUser,
            Place = place
        };

        var createResult = await _placeReviewService.AddAsync(placeReview);
        if (createResult.IsFailed)
            return new BadRequestObjectResult(createResult.GetReport());

        return new OkObjectResult(createResult.Value);
    }
}