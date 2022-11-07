using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class GetPlacesUseCase : PlaceUseCase
{
    private readonly PlaceService _placeService;
    private readonly AuthService _authService;

    public GetPlacesUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceRecord>>> GetAllAsync(ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;
        
        var canViewResult = _placeService.CanViewAll(currentUser);
        if (canViewResult.IsFailed)
            return new ObjectResult(canViewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeWithIdResult = await _placeService.GetAllAsync();
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var places = placeWithIdResult.Value;

        
        return new OkObjectResult(places);
    }
}