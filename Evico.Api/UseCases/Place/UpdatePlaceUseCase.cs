using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class UpdatePlaceUseCase
{
    public async Task<ActionResult<PlaceRecord>> UpdateAsync(UpdatePlaceInputModel inputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}