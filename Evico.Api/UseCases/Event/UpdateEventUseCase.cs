using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class UpdateEventUseCase
{
    private readonly EventService _eventService;
    private readonly AuthService _authService;

    public UpdateEventUseCase(EventService eventService, AuthService authService)
    {
        _eventService = eventService;
        _authService = authService;
    }

    public async Task<ActionResult<EventRecord>> UpdateAsync(UpdateEventInputModel updateEventModel, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;
        
        var eventWithIdResult = await _eventService.GetByIdAsync(updateEventModel.Id);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canUpdateEventResult = _eventService.CanUpdate(eventWithId, currentUser);
        if (canUpdateEventResult.IsFailed)
            return new ObjectResult(canUpdateEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        
        var updateEventResult = await _eventService.Update(updateEventModel);

        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}