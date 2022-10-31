using Evico.Api.Entity;
using Evico.Api.InputModels;
using Evico.Api.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Services;

public class EventService
{
    private readonly ApplicationContext _context;

    public EventService(ApplicationContext context)
    {
        _context = context;
    }

    private EventQueryBuilder _eventQueryBuilder => new(_context);
    private PlaceQueryBuilder _placeQueryBuilder => new(_context);

    public async Task<IActionResult> AddAsync(EventInputModel eventInputModel)
    {
        try
        {
            var place = await _placeQueryBuilder.WithId(eventInputModel.PlaceId).FirstOrDefaultAsync();

            if (place == null)
                throw new InvalidOperationException($"Place cannot be null. Place id: {eventInputModel.PlaceId}");

            var eventRecord = new EventRecord
            {
                Start = eventInputModel.Start,
                End = eventInputModel.End,
                PlaceId = eventInputModel.PlaceId,
                Name = eventInputModel.Name,
                Description = eventInputModel.Description
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