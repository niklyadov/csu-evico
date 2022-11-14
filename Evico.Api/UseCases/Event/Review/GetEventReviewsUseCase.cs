using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review;

public class GetEventReviewsUseCase
{
    private readonly AuthService _authService;
    private readonly EventReviewService _eventReviewService;
    private readonly EventService _eventService;

    public GetEventReviewsUseCase(EventService eventService, EventReviewService eventReviewService,
        AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<List<EventReviewRecord>>> GetAllAsync(long eventId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(
                eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canGetAllReviewsResult = _eventReviewService.CanViewAll(eventWithId, currentUser);
        if (canGetAllReviewsResult.IsFailed)
            return new ObjectResult(canGetAllReviewsResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        var getAllEventsResult = await _eventReviewService.GetAll();
        if (getAllEventsResult.IsFailed)
            return new BadRequestObjectResult(
                getAllEventsResult.GetReport());

        return new OkObjectResult(getAllEventsResult.Value);
    }
}