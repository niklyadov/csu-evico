using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class DeletePlaceCategoryUseCase
{
    public ActionResult<List<PlaceCategoryRecord>> DeleteAsync(long categoryId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}