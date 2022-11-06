using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class DeleteEventByIdUseCase
{
    private readonly EventService _eventService;
    private readonly AuthService _authService;

    public DeleteEventByIdUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<EventRecord>> DeleteById(long eventId, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;
        
        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;
        
        var canDeleteEventResult = _eventService.CanDelete(eventWithId, currentUser);
        if (canDeleteEventResult.IsFailed)
            return new ObjectResult(canDeleteEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var deleteByIdResult = await _eventService.DeleteAsync(eventWithId);
        if (deleteByIdResult.IsFailed)
            return new BadRequestObjectResult(deleteByIdResult.GetReport());

        return new OkObjectResult(deleteByIdResult.Value);
    }
}