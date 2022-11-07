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
    private readonly AuthService _authService;
    private readonly EventService _eventService;
    private readonly PlaceService _placeService;

    public UpdateEventUseCase(EventService eventService, AuthService authService, PlaceService placeService)
    {
        _eventService = eventService;
        _authService = authService;
        _placeService = placeService;
    }

    public async Task<ActionResult<EventRecord>> UpdateAsync(UpdateEventInputModel updateEventModel,
        ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(updateEventModel.Id);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(eventWithIdResult.GetReport());
        var eventRecord = eventWithIdResult.Value;

        var canUpdateEventResult = _eventService.CanUpdate(eventRecord, currentUser);
        if (canUpdateEventResult.IsFailed)
            return new ObjectResult(canUpdateEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (updateEventModel.Start != null)
            eventRecord.Start = updateEventModel.Start;

        if (updateEventModel.End != null)
            eventRecord.End = updateEventModel.End;

        if (updateEventModel.PlaceId != null)
        {
            var placeResult = await _placeService.GetByIdAsync(updateEventModel.PlaceId.Value);
            if (placeResult.IsFailed)
                return new BadRequestObjectResult(placeResult.GetReport());
            var place = placeResult.Value;

            if (place.IsDeleted)
                return new BadRequestObjectResult($"Place with id {place.Id} was deleted");

            eventRecord.Place = place;
        }

        if (!string.IsNullOrEmpty(updateEventModel.Name))
            eventRecord.Name = updateEventModel.Name;

        if (!string.IsNullOrEmpty(updateEventModel.Description))
            eventRecord.Description = updateEventModel.Description;

        var updateEventResult = await _eventService.UpdateAsync(eventRecord);

        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}