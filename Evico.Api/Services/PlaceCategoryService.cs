using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class PlaceCategoryService
{
    private readonly ApplicationContext _applicationContext;
    private PlaceCategoryQueryBuilder _queryBuilder => new(_applicationContext);
    
    public PlaceCategoryService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<PlaceCategoryRecord>> AddAsync(PlaceCategoryRecord category)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.AddAsync(category);
        });
    }
    
    public async Task<Result<PlaceCategoryRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.WithId(id).SingleAsync();
        });
    }
    
    public async Task<Result<List<PlaceCategoryRecord>>> GetAllAsync()
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.ToListAsync();
        });
    }

    public async Task<Result<PlaceCategoryRecord>> UpdateAsync(PlaceCategoryRecord categoryRecord)
    {
        return await Result.Try(async () =>
        {
            return await _queryBuilder.UpdateAsync(categoryRecord);
        });
    }
    
    public async Task<Result<PlaceCategoryRecord>> DeleteAsync(PlaceCategoryRecord categoryRecord)
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

    public Result CanView(PlaceCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if(categoryRecord.IsDeleted)
            return Result.Fail($"Place category with id {categoryRecord} is Deleted");
        
        return Result.Ok();
    }

    public Result CanViewAll(ProfileRecord profileRecord)
    {
        return Result.Ok();
    }

    public Result CanUpdate(PlaceCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if (categoryRecord.IsDeleted)
            return Result.Fail($"Place category with id {categoryRecord} is Deleted");
        
        return Result.Ok();
    }

    public Result CanDelete(PlaceCategoryRecord categoryRecord, ProfileRecord profileRecord)
    {
        if (categoryRecord.IsDeleted)
            return Result.Fail($"Place category with id {categoryRecord} is already Deleted");
        
        return Result.Ok();
    }
}