using Evico.Api.Entity;
using Evico.Api.InputModels;
using Evico.Api.InputModels.Event;
using Evico.Api.UseCases.Event;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : BaseController
{
    private readonly AddEventUseCase _addEventUseCase;
    private readonly GetEventsUseCase _getEventsUseCase;
    private readonly GetEventByIdUseCase _getEventByIdUseCase;
    private readonly UpdateEventByIdUseCase _updateEventByIdUseCase;
    private readonly DeleteEventByIdUseCase _deleteEventByIdUseCase;

    public EventController(IServiceProvider services)
    {
        _addEventUseCase = services.GetRequiredService<AddEventUseCase>();
        _getEventsUseCase = services.GetRequiredService<GetEventsUseCase>();
        _getEventByIdUseCase = services.GetRequiredService<GetEventByIdUseCase>();
        _updateEventByIdUseCase = services.GetRequiredService<UpdateEventByIdUseCase>();
        _deleteEventByIdUseCase = services.GetRequiredService<DeleteEventByIdUseCase>();
    }

    [HttpPost]
    public async Task<ActionResult<EventRecord>> Add([FromBody] AddEventInputModel addEventModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _addEventUseCase.AddEventAsync(addEventModel);
    }

    [HttpGet]
    public async Task<ActionResult<List<EventRecord>>> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventsUseCase.GetAllAsync();
    }
    
    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventRecord>> GetById([FromRoute] long eventId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventByIdUseCase.GetById(eventId);
    }
    
    [HttpPut]
    public async Task<ActionResult<EventRecord>> Update([FromBody] UpdateEventInputModel updateEventModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _updateEventByIdUseCase.Update(updateEventModel);
    }
    
    [HttpDelete("{eventId}")]
    public async Task<ActionResult<EventRecord>> DeleteById([FromRoute] long eventId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _deleteEventByIdUseCase.DeleteById(eventId);
    }
}