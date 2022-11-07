using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class AddPlaceUseCase : PlaceUseCase
{
    public async Task<ActionResult<PlaceRecord>> AddAsync(AddPlaceInputModel addPlaceInputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}