using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class DeleteEventByIdUseCase
{
    private readonly EventService _eventService;

    public DeleteEventByIdUseCase(EventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<ActionResult<EventRecord>> DeleteById(long eventId)
    {
        var deleteByIdResult = await _eventService.DeleteByIdAsync(eventId);

        if (deleteByIdResult.IsFailed)
            return new BadRequestObjectResult(deleteByIdResult.GetReport());

        return new OkObjectResult(deleteByIdResult.Value);
    }
}