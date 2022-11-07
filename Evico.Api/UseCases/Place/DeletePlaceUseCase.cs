using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class DeletePlaceUseCase
{
    private readonly PlaceService _placeService;
    private readonly AuthService _authService;

    public DeletePlaceUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }
    
    public async Task<ActionResult<PlaceRecord>> DeleteByIdAsync(long placeId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeWithIdResult = await _placeService.GetByIdAsync(placeId);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var placeRecord = placeWithIdResult.Value;
        
        var canDeleteResult = _placeService.CanDelete(placeRecord, currentUser);
        if (canDeleteResult.IsFailed)
            return new ObjectResult(canDeleteResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var deletePlaceResult = await _placeService.DeleteAsync(placeRecord);
        if (deletePlaceResult.IsFailed)
            return new BadRequestObjectResult(deletePlaceResult.GetReport());
        var deletedPlace = deletePlaceResult.Value;

        return new OkObjectResult(deletedPlace);
    }
}