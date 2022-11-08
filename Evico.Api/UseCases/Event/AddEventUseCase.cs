using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class AddEventUseCase
{
    private readonly AuthService _authService;
    private readonly EventService _eventService;
    private readonly PlaceService _placeService;

    public AddEventUseCase(EventService eventService, AuthService authService, PlaceService placeService)
    {
        _eventService = eventService;
        _authService = authService;
        _placeService = placeService;
    }

    public async Task<ActionResult<EventRecord>> AddAsync(AddEventInputModel addEventModel, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeWithIdResult = await _placeService.GetByIdAsync(addEventModel.PlaceId);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());

        var canCreateResult = _eventService.CanCreate(placeWithIdResult.Value, currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var eventRecord = new EventRecord
        {
            Start = addEventModel.Start,
            End = addEventModel.End,
            PlaceId = addEventModel.PlaceId,
            Name = addEventModel.Name,
            Description = addEventModel.Description,
            Owner = currentUser
        };

        var addEventResult = await _eventService.AddAsync(eventRecord);
        if (addEventResult.IsFailed)
            return new BadRequestObjectResult(addEventResult.GetReport());

        return new OkObjectResult(addEventResult.Value);
    }
}