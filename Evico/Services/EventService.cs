using Evico.Entity;
using Evico.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Services;

public class EventService
{
    private readonly ApplicationContext _context;
    private EventQueryBuilder _eventQueryBuilder => new (_context);

    public EventService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AddAsync(EventRecord eventRecord)
    {
        try
        {
            var result = await _eventQueryBuilder.AddAsync(eventRecord);

            return new OkObjectResult(result);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }

    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var result = await _eventQueryBuilder.ToListAsync();

            return new OkObjectResult(result);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}