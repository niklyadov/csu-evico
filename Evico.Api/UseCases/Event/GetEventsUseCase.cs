using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventsUseCase : EventUseCase
{
    private readonly EventService _eventService;

    public GetEventsUseCase(EventService eventService)
    {
        _eventService = eventService;
    }
    
    public async Task<ActionResult<List<EventRecord>>> GetAllAsync()
    {
        var getAllResult = await _eventService.GetAllAsync();

        if (getAllResult.IsFailed)
            return new BadRequestObjectResult(getAllResult.GetReport());

        return new OkObjectResult(getAllResult.Value);
    }
}