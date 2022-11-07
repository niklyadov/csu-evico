using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class AddPlaceUseCase : PlaceUseCase
{
    private readonly PlaceService _placeService;
    private readonly AuthService _authService;

    public AddPlaceUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }
    
    public async Task<ActionResult<PlaceRecord>> AddAsync(AddPlaceInputModel inputModel, ClaimsPrincipal claimsPrincipal)
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