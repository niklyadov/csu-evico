using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class EventReviewPhotoService
{
    private readonly ApplicationContext _applicationContext;
    private EventReviewPhotoQueryBuilder PhotoQueryBuilder => new(_applicationContext);

    public EventReviewPhotoService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<EventReviewPhotoRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.WithId(id).SingleAsync();
        });
    }

    public async Task<Result<EventReviewPhotoRecord>> AddAsync(EventReviewPhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.AddAsync(photoRecord);
        });
    }
    
    public async Task<Result<EventReviewPhotoRecord>> DeleteAsync(EventReviewPhotoRecord photoRecord)
    {
        return await Result.Try(async () =>
        {
            return await PhotoQueryBuilder.DeleteAsync(photoRecord);
        });
    }

    public Result CanDelete(EventReviewPhotoRecord photo, ProfileRecord profile)
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