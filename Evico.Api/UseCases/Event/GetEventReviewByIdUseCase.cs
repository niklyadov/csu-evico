using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventReviewByIdUseCase
{
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public GetEventReviewByIdUseCase(EventService eventService, EventReviewService eventReviewService, AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }
    
    public async Task<ActionResult<EventReviewRecord>> GetByIdAsync(long eventId, long reviewId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventReviewByIdResult = await _eventReviewService.GetById(reviewId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;
        
        var canViewEventReviewResult = _eventReviewService.CanView(eventReview, currentUser);
        if (canViewEventReviewResult.IsFailed)
            return new ObjectResult(canViewEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        return new OkObjectResult(eventReview);
    }
}