using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class PlaceReviewService
{
    private readonly ApplicationContext _applicationContext;

    public PlaceReviewService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    private PlaceReviewQueryBuilder PlaceReviewQueryBuilder
        => new(_applicationContext);

    public async Task<Result<PlaceReviewRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await PlaceReviewQueryBuilder
                .WithId(id)
                .SingleAsync();
        });
    }

    public async Task<Result<List<PlaceReviewRecord>>> GetAllAsync()
    {
        return await Result.Try(async () =>
        {
            return await PlaceReviewQueryBuilder
                .ToListAsync();
        });
    }

    public async Task<Result<PlaceReviewRecord>> UpdateAsync(PlaceReviewRecord placeReview)
    {
        return await Result.Try(async () =>
        {
            return await PlaceReviewQueryBuilder
                .UpdateAsync(placeReview);
        });
    }

    public async Task<Result<PlaceReviewRecord>> DeleteAsync(PlaceReviewRecord placeReview)
    {
        return await Result.Try(async () =>
        {
            return await PlaceReviewQueryBuilder
                .DeleteAsync(placeReview);
        });
    }

    public async Task<Result<PlaceReviewRecord>> AddAsync(PlaceReviewRecord placeReview)
    {
        return await Result.Try(async () =>
        {
            return await PlaceReviewQueryBuilder
                .AddAsync(placeReview);
        });
    }

    public Result CanView(PlaceRecord place, PlaceReviewRecord placeReview, ProfileRecord? user)
    {
        if (place.IsDeleted)
            return Result.Fail($"Place with id {place.Id} is deleted");

        return Result.Ok();
    }

    public Result CanViewAll(PlaceRecord place, ProfileRecord? user)
    {
        if (place.IsDeleted)
            return Result.Fail($"Place with id {place.Id} is deleted");

        return Result.Ok();
    }

    public Result CanUpdate(PlaceRecord placeRecord, PlaceReviewRecord placeReview, ProfileRecord profile)
    {
        if (placeRecord.IsDeleted)
            return Result.Fail($"Place with id {placeRecord.Id} is deleted");

        if (placeReview.IsDeleted)
            return Result.Fail($"Place review with id {placeReview.Id} is deleted");

        if (placeRecord.Id != placeReview.PlaceId)
            return Result.Fail($"Place id {placeRecord.Id} must be {placeReview.PlaceId}");
        
        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(placeReview.AuthorId == profile.Id,
            "Only owner or moderator can update this Review");
    }

    public Result CanDelete(PlaceRecord placeRecord, PlaceReviewRecord placeReview, ProfileRecord profile)
    {
        if (placeRecord.IsDeleted)
            return Result.Fail($"Place with id {placeRecord.Id} is deleted");

        if (placeReview.IsDeleted)
            return Result.Fail($"Place review with id {placeReview.Id} is deleted");

        if (placeRecord.Id != placeReview.PlaceId)
            return Result.Fail($"Place id {placeRecord.Id} must be {placeReview.PlaceId}");
        
        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(placeReview.AuthorId == profile.Id,
            "Only owner or moderator can delete this Review");
    }

    public Result CanCreate(PlaceRecord place, ProfileRecord user)
    {
        if (place.IsDeleted)
            return Result.Fail($"Place with id {place.Id} is deleted");

        return Result.Ok();
    }
}