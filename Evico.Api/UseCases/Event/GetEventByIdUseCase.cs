using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventByIdUseCase
{
    private readonly EventService _eventService;

    public GetEventByIdUseCase(EventService eventService)
    {
        _eventService = eventService;
    }
    
    public async Task<ActionResult<EventRecord>> GetById(long eventId)
    {
        var getByIdResult = await _eventService.GetByIdAsync(eventId);

        if (getByIdResult.IsFailed)
            return new BadRequestObjectResult(getByIdResult.GetReport());

        return new OkObjectResult(getByIdResult.Value);
    }
}