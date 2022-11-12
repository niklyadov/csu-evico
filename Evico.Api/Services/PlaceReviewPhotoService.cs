using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class PlaceReviewPhotoService
{
    private readonly ApplicationContext _applicationContext;
    private PlaceReviewPhotoQueryBuilder PhotoQueryBuilder => new(_applicationContext);

    public PlaceReviewPhotoService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<PlaceReviewPhotoRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.WithId(id).SingleAsync();
        });
    }

    public async Task<Result<PlaceReviewPhotoRecord>> AddAsync(PlaceReviewPhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.AddAsync(photoRecord);
        });
    }
    
    public async Task<Result<PlaceReviewPhotoRecord>> DeleteAsync(PlaceReviewPhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.DeleteAsync(photoRecord);
        });
    }

    public Result CanDelete(PlaceReviewPhotoRecord photo, ProfileRecord profile)
    {
        // todo: добавить проверку: модератор тоже может удалить это фото
        
        return Result.OkIf(photo.AuthorId == profile.Id, 
            new Error("Only author or moderator can delete this photo"));
    }
    
    public Result CanUpload(ProfileRecord profile)
    {
        return Result.Ok();
    }
}