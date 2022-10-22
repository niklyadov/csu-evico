using Evico.Entity;
using Evico.QueryBuilder;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Services;

public class PlaceService
{
    private readonly PlaceQueryBuilder _placeQueryBuilder;

    public PlaceService(PlaceQueryBuilder placeQueryBuilder)
    {
        _placeQueryBuilder = placeQueryBuilder;
    }
    
    public async Task<IActionResult> AddAsync(PlaceRecord placeRecord)
    {
        try
        {
            var result = await _placeQueryBuilder.AddAsync(placeRecord);

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
            var result = await _placeQueryBuilder.ToListAsync();

            return new OkObjectResult(result);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}