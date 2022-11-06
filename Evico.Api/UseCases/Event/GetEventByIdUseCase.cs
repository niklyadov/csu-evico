using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventByIdUseCase
{
    private readonly EventService _eventService;
    private readonly AuthService _authService;

    public GetEventByIdUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<EventRecord>> GetByIdAsync(long eventId, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var getByIdResult = await _eventService.GetByIdAsync(eventId);
        if (getByIdResult.IsFailed)
            return new BadRequestObjectResult(getByIdResult.GetReport());
        var eventWithId = getByIdResult.Value;
        
        var canViewEventResult = _eventService.CanView(eventWithId, currentUser);
        if (canViewEventResult.IsFailed)
            return new ObjectResult(canViewEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        return new OkObjectResult(eventWithId);
    }
}