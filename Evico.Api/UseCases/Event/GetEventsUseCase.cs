using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventsUseCase : EventUseCase
{
    private readonly EventService _eventService;
    private readonly AuthService _authService;

    public GetEventsUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<List<EventRecord>>> GetAllAsync(ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var canViewAllResult = _eventService.CanViewAll(currentUser);
        if(!canViewAllResult.IsFailed)
            return new ObjectResult(canViewAllResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var getAllResult = await _eventService.GetAllAsync();

        if (getAllResult.IsFailed)
            return new BadRequestObjectResult(getAllResult.GetReport());

        return new OkObjectResult(getAllResult.Value);
    }
}