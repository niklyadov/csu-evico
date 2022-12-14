using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class PlacePhotoService
{
    private readonly ApplicationContext _applicationContext;

    public PlacePhotoService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    private PlacePhotoQueryBuilder PhotoQueryBuilder => new(_applicationContext);

    public async Task<Result<PlacePhotoRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () => { return await PhotoQueryBuilder.WithId(id).SingleAsync(); });
    }

    public async Task<Result<PlacePhotoRecord>> AddAsync(PlacePhotoRecord photoRecord)
    {
        return await Result.Try(async () => { return await PhotoQueryBuilder.AddAsync(photoRecord); });
    }

    public async Task<Result<PlacePhotoRecord>> DeleteAsync(PlacePhotoRecord photoRecord)
    {
        return await Result.Try(async () => { return await PhotoQueryBuilder.DeleteAsync(photoRecord); });
    }

    public Result CanDelete(PlacePhotoRecord photo, ProfileRecord profile)
    {
        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(photo.AuthorId == profile.Id, 
            new Error("Only author or moderator can delete this photo"));
    }

    public Result CanUpload(ProfileRecord profile)
    {
        return Result.Ok();
    }
}