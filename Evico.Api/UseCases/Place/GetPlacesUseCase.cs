using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class GetPlacesUseCase : PlaceUseCase
{
    public async Task<ActionResult<List<PlaceRecord>>> GetAllAsync(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}