using Evico.Entity;
using Evico.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Controllers;

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
    public async Task<IActionResult> Add([FromBody] EventRecord eventRecord)
    {
        return await _eventService.AddAsync(eventRecord);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _eventService.GetAllAsync();
    }
}