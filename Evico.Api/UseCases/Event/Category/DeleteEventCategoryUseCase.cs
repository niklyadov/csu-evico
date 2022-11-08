using System.Security.Claims;
using Evico.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Category;

public class DeleteEventCategoryUseCase
{
    public ActionResult<List<EventCategoryRecord>> DeleteAsync(long categoryId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}