using Evico.Api.Entity;
using Evico.Api.InputModels;
using Evico.Api.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Services;

public class EventCategoryService
{
    private readonly ApplicationContext _context;

    public EventCategoryService(ApplicationContext context)
    {
        _context = context;
    }

    private EventQueryBuilder _eventQueryBuilder => new(_context);
    private CategoryQueryBuilder _categoryQueryBuilder => new(_context);

    public async Task<IActionResult> AddAsync(EventCategoryInputModel eventCategoryInputModel)
    {
        try
        {
            var event = await _eventQueryBuilder.WithId(eventCategoryInputModel.EventId).FirstOrDefaultAsync();

            if (event == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {eventCategoryInputModel.EventId}");

            var category = await _categoryQueryBuilder.WithId(eventCategoryInputModel.CategoryId).FirstOrDefaultAsync();
            
            if (category == null)
                throw new InvalidOperationException($"Category cannot be null. Category id: {eventCategoryInputModel.CategoryId}");

            category.Events.Add(event);
            
            var result = _categoryQueryBuilder.UpdateAsync(category);

            return new OkObjectResult(result);

        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }

    public async Task<IActionResult> RemoveAsync(EventCategoryInputModel eventCategoryInputModel)
    {
        try
        {
            var event = await _eventQueryBuilder.WithId(eventCategoryInputModel.EventId).FirstOrDefaultAsync();

            if (event == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {eventCategoryInputModel.EventId}");

            var category = await _categoryQueryBuilder.WithId(eventCategoryInputModel.CategoryId).FirstOrDefaultAsync();
            
            if (category == null)
                throw new InvalidOperationException($"Category cannot be null. Category id: {eventCategoryInputModel.CategoryId}");

            category.Events = category.Events.Where(x => x.Id != event.Id);
            
            var result = _categoryQueryBuilder.UpdateAsync(category);

            return new OkObjectResult(result);

        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}