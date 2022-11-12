using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class ProfilePhotoService
{
    private readonly ApplicationContext _applicationContext;
    private ProfilePhotoQueryBuilder _photoQueryBuilder => new(_applicationContext);

    public ProfilePhotoService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<ProfilePhotoRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await _photoQueryBuilder.WithId(id).SingleAsync();
        });
    }

    public async Task<Result<ProfilePhotoRecord>> AddAsync(ProfilePhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await _photoQueryBuilder.AddAsync(photoRecord);
        });
    }
    
    public async Task<Result<ProfilePhotoRecord>> DeleteAsync(ProfilePhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await _photoQueryBuilder.DeleteAsync(photoRecord);
        });
    }

    public Result CanDelete(ProfilePhotoRecord photo, ProfileRecord profile)
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