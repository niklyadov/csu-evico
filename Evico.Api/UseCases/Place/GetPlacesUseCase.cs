using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class GetPlacesUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceService _placeService;

    public GetPlacesUseCase(PlaceService placeService, AuthService authService)
    {
        _placeService = placeService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceRecord>>> SearchAsync(PlaceSearchInputModel inputMode, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        var currentUser = currentUserResult.ValueOrDefault;

        var canViewResult = _placeService.CanViewAll(currentUser);
        if (canViewResult.IsFailed)
            return new ObjectResult(canViewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeWithIdResult = await _placeService.SearchAsync(inputMode);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());
        var places = placeWithIdResult.Value;


        return new OkObjectResult(places);
    }
}