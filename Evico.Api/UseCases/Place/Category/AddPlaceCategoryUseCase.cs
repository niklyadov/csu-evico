using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class AddPlaceCategoryUseCase
{
    public ActionResult<PlaceCategoryRecord> AddAsync(AddPlaceCategoryInputModel inputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}