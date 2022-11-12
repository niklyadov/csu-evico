using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
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

    public async Task<Result<EventReviewRecord>> GetByIdAsync(long id)
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

    public Result CanUpdate(EventRecord eventRecord, EventReviewRecord eventReviewRecord, ProfileRecord profile)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        if (eventReviewRecord.IsDeleted)
            return Result.Fail("This review is deleted");

        if (eventRecord.Id != eventReviewRecord.EventId)
            return Result.Fail($"Place id {eventRecord.Id} must be {eventReviewRecord.EventId}");
        
        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(eventReviewRecord.AuthorId == profile.Id,
            "Only author or moderator can update this review");
    }

    public Result CanDelete(EventRecord eventRecord, EventReviewRecord eventReviewRecord, ProfileRecord profile)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail("This event is deleted");

        if (eventRecord.Id != eventReviewRecord.EventId)
            return Result.Fail($"Place id {eventRecord.Id} must be {eventReviewRecord.EventId}");
        
        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(eventReviewRecord.AuthorId == profile.Id,
            "Only author or moderator can delete this review");
    }
}