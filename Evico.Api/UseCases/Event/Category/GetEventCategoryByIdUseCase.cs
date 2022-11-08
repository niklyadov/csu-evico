using System.Security.Claims;
using Evico.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Category;

public class GetEventCategoryByIdUseCase
{
    public ActionResult<EventCategoryRecord> GetByIdAsync(long categoryId, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}