using Evico.Entity;
using Evico.InputModels;
using Evico.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Services;

public class PlaceService
{
    private readonly ApplicationContext _context;

    public PlaceService(ApplicationContext context)
    {
        _context = context;
    }

    private PlaceQueryBuilder _placeQueryBuilder => new(_context);

    public async Task<IActionResult> AddAsync(PlaceInputModel placeInputModel)
    {
        try
        {
            var placeRecord = new PlaceRecord
            {
                LocationLatitude = placeInputModel.LocationLatitude,
                LocationLongitude = placeInputModel.LocationLongitude,
                Name = placeInputModel.Name,
                Description = placeInputModel.Description
            };

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