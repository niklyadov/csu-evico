using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Evico.Api.QueryBuilder;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Services;

public class PlaceService
{
    private readonly ApplicationContext _context;

    public PlaceService(ApplicationContext context)
    {
        _context = context;
    }

    private PlaceQueryBuilder _placeQueryBuilder => new(_context);

    public async Task<IActionResult> AddAsync(AddPlaceInputModel addPlaceInputModel)
    {
        try
        {
            var placeRecord = new PlaceRecord
            {
                LocationLatitude = addPlaceInputModel.LocationLatitude,
                LocationLongitude = addPlaceInputModel.LocationLongitude,
                Name = addPlaceInputModel.Name,
                Description = addPlaceInputModel.Description
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

    public async Task<Result<PlaceRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await _placeQueryBuilder.WithId(id).SingleAsync();
        });
    }
}