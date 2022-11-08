using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Category;

public class UpdateEventCategoryUseCase
{
    public ActionResult<List<EventCategoryRecord>> UpdateAsync(UpdateEventCategoryInputModel inputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}