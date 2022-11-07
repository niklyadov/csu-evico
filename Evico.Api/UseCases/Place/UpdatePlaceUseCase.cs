using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class UpdatePlaceUseCase
{
    private readonly PlaceService _placeService;
    private readonly AuthService _authService;

    public UpdatePlaceUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }
    
    public async Task<ActionResult<PlaceRecord>> UpdateAsync(UpdatePlaceInputModel inputModel, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeWithIdResult = await _placeService.GetByIdAsync(inputModel.Id);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var place = placeWithIdResult.Value;
        
        var canUpdateResult = _placeService.CanUpdate(place, currentUser);
        if (canUpdateResult.IsFailed)
            return new ObjectResult(canUpdateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (!String.IsNullOrEmpty(inputModel.Name))
            place.Name = inputModel.Name;
        
        if (!String.IsNullOrEmpty(inputModel.Description))
            place.Description = inputModel.Description;
        
        if (inputModel.LocationLatitude.HasValue)
            place.LocationLatitude = inputModel.LocationLatitude.Value;
        
        if (inputModel.LocationLongitude.HasValue)
            place.LocationLongitude = inputModel.LocationLongitude.Value;
        
        var updatePlaceResult = await _placeService.UpdateAsync(place);
        if (updatePlaceResult.IsFailed)
            return new BadRequestObjectResult(updatePlaceResult.GetReport());
        var deletedPlace = updatePlaceResult.Value;

        return new OkObjectResult(deletedPlace);
    }
}