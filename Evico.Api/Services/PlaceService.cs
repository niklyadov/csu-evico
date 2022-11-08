using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class PlaceService
{
    private readonly ApplicationContext _context;

    public PlaceService(ApplicationContext context)
    {
        _context = context;
    }

    private PlaceQueryBuilder _placeQueryBuilder => new(_context);

    public async Task<Result<PlaceRecord>> AddAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await _placeQueryBuilder.AddAsync(place); });
    }

    public async Task<Result<List<PlaceRecord>>> GetAllAsync()
    {
        return await Result.Try(async () => { return await _placeQueryBuilder.ToListAsync(); });
    }

    public async Task<Result<PlaceRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () => { return await _placeQueryBuilder.WithId(id).SingleAsync(); });
    }

    public async Task<Result<PlaceRecord>> UpdateAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await _placeQueryBuilder.UpdateAsync(place); });
    }

    public async Task<Result<PlaceRecord>> DeleteAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await _placeQueryBuilder.DeleteAsync(place); });
    }

    public Result CanView(PlaceRecord place, ProfileRecord? profile)
    {
        if (place.IsDeleted)
            return Result.Fail($"Event with id: {place.Id} is deleted");

        return Result.Ok();
    }

    public Result CanViewAll(ProfileRecord? profile)
    {
        return Result.Ok();
    }

    public Result CanDelete(PlaceRecord place, ProfileRecord profile)
    {
        if (place.IsDeleted)
            return Result.Fail($"Event with id: {place.Id} was already deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь удалять места
        // todo что будет с событиями если удалить место?

        return Result.OkIf(place.OwnerId == profile.Id,
            "Only owner or moderator can delete this Place");
    }

    public Result CanUpdate(PlaceRecord place, ProfileRecord profile)
    {
        if (place.IsDeleted)
            return Result.Fail($"Event with id: {place.Id} is deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь изменять места

        return Result.OkIf(place.OwnerId == profile.Id,
            "Only owner or moderator can delete this Place");
    }

    public Result CanCreate(PlaceRecord place, ProfileRecord profile)
    {
        return Result.Ok();
    }
}