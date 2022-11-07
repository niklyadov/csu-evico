using Evico.Api.Entity;
using Evico.Api.QueryBuilder;
using FluentResults;

namespace Evico.Api.Services;

public class EventReviewService
{
    private readonly EventReviewQueryBuilder _eventReviewQueryBuilder;

    public EventReviewService(EventReviewQueryBuilder eventReviewQueryBuilder)
    {
        _eventReviewQueryBuilder = eventReviewQueryBuilder;
    }

    public async Task<Result<EventReviewRecord>> Add(EventReviewRecord eventReviewRecord)
    {
        return await Result.Try(async () => { return await _eventReviewQueryBuilder.AddAsync(eventReviewRecord); });
    }

    public async Task<Result<EventReviewRecord>> Update(EventReviewRecord eventReviewRecord)
    {
        return await Result.Try(async () => { return await _eventReviewQueryBuilder.AddAsync(eventReviewRecord); });
    }

    public async Task<Result<EventReviewRecord>> Delete(EventReviewRecord eventReviewRecord)
    {
        return await Result.Try(async () => { return await _eventReviewQueryBuilder.AddAsync(eventReviewRecord); });
    }

    public async Task<Result<EventReviewRecord>> GetById(long id)
    {
        return await Result.Try(async () =>
        {
            return await _eventReviewQueryBuilder
                .WithId(id)
                .Include(x => x.Photos)
                .Include(x => x.Author)
                .SingleAsync();
        });
    }

    public async Task<Result<List<EventReviewRecord>>> GetAll()
    {
        return await Result.Try(async () =>
        {
            return await _eventReviewQueryBuilder
                .ToListAsync();
        });
    }

    public Result CanView(EventRecord eventRecord, EventReviewRecord eventReviewRecord, ProfileRecord? profileRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        if (eventReviewRecord.IsDeleted)
            return Result.Fail("This Review is deleted");

        return Result.Ok();
    }

    public Result CanViewAll(EventRecord eventRecord, ProfileRecord? profileRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        return Result.Ok();
    }

    public Result CanCreate(EventRecord eventRecord, ProfileRecord profileRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        return Result.Ok();
    }

    public Result CanUpdate(EventRecord eventRecord, EventReviewRecord eventReviewRecord, ProfileRecord profileRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь обновлять отзывы

        return Result.OkIf(eventReviewRecord.AuthorId == profileRecord.Id,
            "Only author or moderator can update this review");
    }

    public Result CanDelete(EventRecord eventRecord, EventReviewRecord eventReviewRecord, ProfileRecord profileRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь удалять отзывы

        return Result.OkIf(eventReviewRecord.AuthorId == profileRecord.Id,
            "Only author or moderator can delete this review");
    }
}