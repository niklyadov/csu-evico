using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review;

public class DeleteEventReviewUseCase
{
    private readonly AuthService _authService;
    private readonly EventReviewService _eventReviewService;
    private readonly EventService _eventService;

    public DeleteEventReviewUseCase(EventService eventService, EventReviewService eventReviewService,
        AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<EventReviewRecord>> DeleteByIdAsync(long eventId, long reviewId,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventReviewByIdResult = await _eventReviewService.GetById(reviewId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canDeleteEventReviewResult = _eventReviewService.CanDelete(eventWithId, eventReview, currentUser);
        if (canDeleteEventReviewResult.IsFailed)
            return new ObjectResult(canDeleteEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var deleteEventResult = await _eventReviewService.Delete(eventReview);
        if (deleteEventResult.IsFailed)
            return new BadRequestObjectResult(deleteEventResult.GetReport());

        return new OkObjectResult(deleteEventResult.Value);
    }
}