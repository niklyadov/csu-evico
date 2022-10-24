using Evico.Entity;
using Evico.Models;
using Evico.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Services;

public class EventService
{
    private readonly ApplicationContext _context;
    private EventQueryBuilder _eventQueryBuilder => new (_context);
    private PlaceQueryBuilder _placeQueryBuilder => new (_context);

    public EventService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AddAsync(EventModel eventModel)
    {
        try
        {
            var place = await _placeQueryBuilder.WithId(eventModel.PlaceId).FirstOrDefaultAsync();

            if (place == null)
                throw new InvalidOperationException($"Place cannot be null. Place id: {eventModel.PlaceId}");

            var eventRecord = new EventRecord
            {
                Start = eventModel.Start,
                End = eventModel.End,
                PlaceId = eventModel.PlaceId,
                Name = eventModel.Name,
                Description = eventModel.Description
            };
            
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
            var result = await _eventQueryBuilder.Include(x => x.Place).ToListAsync();

            return new OkObjectResult(result);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}