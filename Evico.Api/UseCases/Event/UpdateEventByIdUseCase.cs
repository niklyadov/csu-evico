using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class UpdateEventByIdUseCase
{
    private readonly EventService _eventService;

    public UpdateEventByIdUseCase(EventService eventService)
    {
        _eventService = eventService;
    }
    
    public async Task<ActionResult<EventRecord>> Update(UpdateEventInputModel updateEventModel)
    {
        var updateEventResult = await _eventService.Update(updateEventModel);

        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}