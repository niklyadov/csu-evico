using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class AddPlaceUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceService _placeService;

    public AddPlaceUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }

    public async Task<ActionResult<PlaceRecord>> AddAsync(AddPlaceInputModel inputModel,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeRecord = new PlaceRecord
        {
            LocationLatitude = inputModel.LocationLatitude,
            LocationLongitude = inputModel.LocationLongitude,
            Name = inputModel.Name,
            Description = inputModel.Description,
            Owner = currentUser
        };

        if (inputModel.ParentId.HasValue)
        {
            var parentPlaceResult = await _placeService.GetByIdAsync(inputModel.ParentId.Value);
            if (parentPlaceResult.IsFailed)
            {
                var parentPlaceError = new Error("Can`t add parent place")
                    .CausedBy(parentPlaceResult.Errors);
                
                return new BadRequestObjectResult(Result.Fail(parentPlaceError).GetReport());
            }
            var parentPlace = parentPlaceResult.Value;

            placeRecord.Parent = parentPlace;
        }

        var canCreateResult = _placeService.CanCreate(placeRecord, currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeCreateResult = await _placeService.AddAsync(placeRecord);
        if (placeCreateResult.IsFailed)
            return new BadRequestObjectResult(placeCreateResult.GetReport());

        var createdPlace = placeCreateResult.Value;

        return new OkObjectResult(createdPlace);
    }
}