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

    public async Task<IActionResult> AddAsync(CategoryInputModel eventCategoryInputModel)
    {
        try
        {
            var eventRecord = await _eventQueryBuilder.WithId(eventCategoryInputModel.EventId).FirstOrDefaultAsync();

            if (eventRecord == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {eventCategoryInputModel.EventId}");

            var category = await _categoryQueryBuilder.WithId(eventCategoryInputModel.CategoryId).FirstOrDefaultAsync();
            
            if (category == null)
                throw new InvalidOperationException($"Category cannot be null. Category id: {eventCategoryInputModel.CategoryId}");

            category.Events.Add(eventRecord);
            
            var result = _categoryQueryBuilder.UpdateAsync(category);

            return new OkObjectResult(result);

        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }

    public async Task<IActionResult> RemoveAsync(CategoryInputModel eventCategoryInputModel)
    {
        try
        {
            var eventRecord = await _eventQueryBuilder.WithId(eventCategoryInputModel.EventId).FirstOrDefaultAsync();

            if (eventRecord == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {eventCategoryInputModel.EventId}");

            var category = await _categoryQueryBuilder.WithId(eventCategoryInputModel.CategoryId).FirstOrDefaultAsync();
            
            if (category == null)
                throw new InvalidOperationException($"Category cannot be null. Category id: {eventCategoryInputModel.CategoryId}");

            category.Events = category.Events.Where(x => x.Id != eventRecord.Id).ToList();
            
            var result = _categoryQueryBuilder.UpdateAsync(category);

            return new OkObjectResult(result);

        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}