using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class EventCategoryService
{
    private readonly ApplicationContext _applicationContext;
    private EventCategoryQueryBuilder _queryBuilder => new(_applicationContext);
    
    public EventCategoryService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<EventCategoryRecord>> AddAsync(EventCategoryRecord category)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.AddAsync(category);
        });
    }
    
    public async Task<Result<EventCategoryRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.WithId(id).SingleAsync();
        });
    }
    
    public async Task<Result<List<EventCategoryRecord>>> GetAllAsync()
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.ToListAsync();
        });
    }
    
    public async Task<Result<List<EventCategoryRecord>>> GetByIdsAsync(List<long> categoryIds)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.WithIds(categoryIds).ToListAsync();
        });
    }

    public async Task<Result<EventCategoryRecord>> UpdateAsync(EventCategoryRecord categoryRecord)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.UpdateAsync(categoryRecord);
        });
    }
    
    public async Task<Result<EventCategoryRecord>> DeleteAsync(EventCategoryRecord categoryRecord)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.DeleteAsync(categoryRecord);
        });
    }
    
    public Result CanCreate(ProfileRecord profileRecord)
    {
        return Result.Ok();
    }

    public Result CanView(EventCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if(categoryRecord.IsDeleted)
            return Result.Fail($"Event category with id {categoryRecord} is Deleted");
        
        return Result.Ok();
    }

    public Result CanViewAll(ProfileRecord profileRecord)
    {
        return Result.Ok();
    }

    public Result CanUpdate(EventCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if (categoryRecord.IsDeleted)
            return Result.Fail($"Event category with id {categoryRecord} is Deleted");
        
        return Result.Ok();
    }

    public Result CanDelete(EventCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if (categoryRecord.IsDeleted)
            return Result.Fail($"Event category with id {categoryRecord} is already Deleted");
        
        return Result.Ok();
    }
}