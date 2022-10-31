using Evico.Api.InputModels;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : BaseController
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] EventInputModel eventModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _eventService.AddAsync(eventModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _eventService.GetAllAsync();
    }
}