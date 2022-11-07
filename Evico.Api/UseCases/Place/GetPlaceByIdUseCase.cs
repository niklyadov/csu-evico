using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class GetPlaceByIdUseCase
{
    public async Task<ActionResult<PlaceRecord>> GetByIdAsync(long placeId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}