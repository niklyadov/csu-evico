using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class AddEventUseCase : EventUseCase
{
    private readonly EventService _eventService;
    private readonly AuthService _authService;

    public AddEventUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<EventRecord>> AddAsync(AddEventInputModel addEventModel, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var canCreateEventResult = _eventService.CanCreate(currentUser);
        if (canCreateEventResult.IsFailed)
            return new ObjectResult(canCreateEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var addEventResult = await _eventService.AddAsync(addEventModel, currentUser);

        if (addEventResult.IsFailed)
            return new BadRequestObjectResult(addEventResult.GetReport());

        return new OkObjectResult(addEventResult.Value);
    }
}