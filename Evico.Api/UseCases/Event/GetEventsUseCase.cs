using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventsUseCase
{
    private readonly AuthService _authService;
    private readonly EventService _eventService;

    public GetEventsUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<List<EventRecord>>> SearchAsync(EventSearchFilters filters, 
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var canViewAllResult = _eventService.CanViewAll(currentUser);
        if (canViewAllResult.IsFailed)
            return new ObjectResult(canViewAllResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var searchResult = await _eventService.SearchAsync(filters);

        if (searchResult.IsFailed)
            return new BadRequestObjectResult(searchResult.GetReport());

        return new OkObjectResult(searchResult.Value);
    }
}