using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class AddEventUseCase : EventUseCase
{
    private readonly EventService _eventService;

    public AddEventUseCase(EventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<ActionResult<EventRecord>> AddEventAsync(AddEventInputModel addEventModel)
    {
        var addEventResult = await _eventService.AddAsync(addEventModel);

        if (addEventResult.IsFailed)
            return new BadRequestObjectResult(addEventResult.GetReport());

        return new OkObjectResult(addEventResult.Value);
    }
}