using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class DeleteEventReviewUseCase
{
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public DeleteEventReviewUseCase(EventService eventService, EventReviewService eventReviewService, AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }
    
    public async Task<ActionResult<EventReviewRecord>> DeleteByIdAsync(long eventId, long reviewId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventReviewByIdResult = await _eventReviewService.GetById(reviewId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;

        var canDeleteEventReviewResult = _eventReviewService.CanDelete(eventReview, currentUser);
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