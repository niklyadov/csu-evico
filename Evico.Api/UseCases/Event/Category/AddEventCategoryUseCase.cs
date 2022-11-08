using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Category;

public class AddEventCategoryUseCase
{
    public ActionResult<EventCategoryRecord> AddAsync(AddEventCategoryInputModel inputModel, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}