using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Category;

public class GetEventCategoriesUseCase
{
    public ActionResult<List<EventCategoryRecord>> GetAllAsync(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}