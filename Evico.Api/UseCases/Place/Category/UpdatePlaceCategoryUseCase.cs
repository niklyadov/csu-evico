using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.InputModels.Place;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class UpdatePlaceCategoryUseCase
{
    public ActionResult<List<PlaceCategoryRecord>> UpdateAsync(UpdatePlaceCategoryInputModel inputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}