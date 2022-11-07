using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class DeletePlaceUseCase
{
    public async Task<ActionResult<PlaceRecord>> DeleteByIdAsync(long placeId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}