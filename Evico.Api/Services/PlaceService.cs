using Evico.Api.Entities;
using Evico.Api.InputModels.Place;
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

    private PlaceQueryBuilder PlaceQueryBuilder => new(_context);

    public async Task<Result<PlaceRecord>> AddAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await PlaceQueryBuilder.AddAsync(place); });
    }

    public async Task<Result<List<PlaceRecord>>> SearchAsync(PlaceSearchInputModel filters)
    {
        return await Result.Try(async () =>
        {
            PlaceQueryBuilder placeQueryBuilder = (PlaceQueryBuilder)PlaceQueryBuilder
                .Include(x => x.Categories)
                .WhereNotDeleted();

            placeQueryBuilder = WithOwnerId(placeQueryBuilder, filters);
            placeQueryBuilder = WithSearchQueryFilter(placeQueryBuilder, filters);
            placeQueryBuilder = WithInCategoriesFilter(placeQueryBuilder, filters);
            placeQueryBuilder = WithNotInCategoriesFilter(placeQueryBuilder, filters);
            placeQueryBuilder = placeQueryBuilder.Sort(filters.SortBy, filters.SortOrder);
            placeQueryBuilder = (PlaceQueryBuilder)placeQueryBuilder.Skip(filters.Offset);
            placeQueryBuilder = (PlaceQueryBuilder)placeQueryBuilder.Limit(filters.Limit);
            
            return await placeQueryBuilder.ToListAsync();
        });
    }
    
    private PlaceQueryBuilder WithSearchQueryFilter(PlaceQueryBuilder queryBuilder, PlaceSearchInputModel filters)
    {
        if (!String.IsNullOrEmpty(filters.SearchQuery))
        {
            return queryBuilder.SearchString(filters.SearchQuery);
        }
        
        return queryBuilder;
    }
    
    private PlaceQueryBuilder WithOwnerId(PlaceQueryBuilder queryBuilder, PlaceSearchInputModel filters)
    {
        if (filters.OwnerId.HasValue)
        {
            return queryBuilder.WithOwnerId(filters.OwnerId.Value);
        }
        
        return queryBuilder;
    }
    
    private PlaceQueryBuilder WithInCategoriesFilter(PlaceQueryBuilder queryBuilder, PlaceSearchInputModel filters)
    { 
        if (!String.IsNullOrEmpty(filters.InCategories))
        {
            var inCategories = filters.InCategories.Split(',')
                .Select(x=> long.Parse(x)).ToList();

            return queryBuilder.WhereCategoriesIn(inCategories);
        }

        return queryBuilder;
    }

    private PlaceQueryBuilder WithNotInCategoriesFilter(PlaceQueryBuilder queryBuilder, PlaceSearchInputModel filters)
    {
        if (!String.IsNullOrEmpty(filters.NotInCategories))
        {
            var notInCategories = filters.NotInCategories.Split(',')
                .Select(x=> long.Parse(x)).ToList();

            return queryBuilder.WhereCategoriesNotIn(notInCategories);
        }

        return queryBuilder;
    }

    public async Task<Result<PlaceRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await PlaceQueryBuilder
                .WithId(id)
                .Include(x => x.Categories)
                .SingleAsync();
        });
    }

    public async Task<Result<PlaceRecord>> UpdateAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await PlaceQueryBuilder.UpdateAsync(place); });
    }

    public async Task<Result<PlaceRecord>> DeleteAsync(PlaceRecord place)
    {
        return await Result.Try(async () => { return await PlaceQueryBuilder.DeleteAsync(place); });
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

        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        // todo что будет с событиями если удалить место?

        return Result.OkIf(place.OwnerId == profile.Id,
            "Only owner or moderator can delete this Place");
    }

    public Result CanUpdate(PlaceRecord place, ProfileRecord profile)
    {
        if (place.IsDeleted)
            return Result.Fail($"Event with id: {place.Id} is deleted");

        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();

        return Result.OkIf(place.OwnerId == profile.Id,
            "Only owner or moderator can delete this Place");
    }

    public Result CanCreate(PlaceRecord place, ProfileRecord profile)
    {
        return Result.Ok();
    }
}