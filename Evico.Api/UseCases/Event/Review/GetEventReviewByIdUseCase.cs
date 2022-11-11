using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review;

public class GetEventReviewByIdUseCase
{
    private readonly AuthService _authService;
    private readonly EventReviewService _eventReviewService;
    private readonly EventService _eventService;

    public GetEventReviewByIdUseCase(EventService eventService, EventReviewService eventReviewService,
        AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<EventReviewRecord>> GetByIdAsync(long eventId, long reviewId,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var eventReviewByIdResult = await _eventReviewService.GetByIdAsync(reviewId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canViewEventReviewResult = _eventReviewService.CanView(eventWithId, eventReview, currentUser);
        if (canViewEventReviewResult.IsFailed)
            return new ObjectResult(canViewEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        return new OkObjectResult(eventReview);
    }
}