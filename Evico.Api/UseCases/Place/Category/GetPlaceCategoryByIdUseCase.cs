using System.Security.Claims;
using Evico.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class GetPlaceCategoryByIdUseCase
{
    public ActionResult<PlaceCategoryRecord> GetByIdAsync(long categoryId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}