using System.Security.Claims;
using Evico.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class GetPlaceCategoriesUseCase
{
    public ActionResult<List<PlaceCategoryRecord>> GetAllAsync(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}