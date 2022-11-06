using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class AddEventReviewUseCase
{
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public AddEventReviewUseCase(EventService eventService, EventReviewService eventReviewService, AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }
    
    public async Task<ActionResult<EventReviewRecord>> AddAsync(long eventId, AddEventReviewInputModel inputModel, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(
                eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;
        
        var canCreateReviewResult = _eventReviewService.CanCreate(eventWithId, currentUser);
        if (canCreateReviewResult.IsFailed)
            return new ObjectResult(canCreateReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        throw new NotImplementedException();
    }
}