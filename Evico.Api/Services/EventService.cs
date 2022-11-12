using Evico.Api.Entities;
using Evico.Api.QueryBuilders;
using FluentResults;

namespace Evico.Api.Services;

public class EventService
{
    private readonly ApplicationContext _context;

    public EventService(ApplicationContext context)
    {
        _context = context;
    }

    private EventQueryBuilder EventQueryBuilder => new(_context);

    public async Task<Result<EventRecord>> AddAsync(EventRecord eventRecord)
    {
        return await Result.Try(async () => { return await EventQueryBuilder.AddAsync(eventRecord); });
    }

    public async Task<Result<List<EventRecord>>> GetAllAsync()
    {
        return await Result.Try(async () =>
        {
            return await EventQueryBuilder
                .Include(x => x.Categories)
                .WhereNotDeleted()
                .ToListAsync();
        });
    }

    public async Task<Result<EventRecord>> GetByIdAsync(long eventId)
    {
        return await Result.Try(async () =>
        {
            return await EventQueryBuilder
                .Include(x => x.Categories)
                .Include(x => x.Place)
                .WhereNotDeleted()
                .WithId(eventId)
                .SingleAsync();
        });
    }

    public async Task<Result<EventRecord>> DeleteByIdAsync(long eventId)
    {
        return await Result.Try(async () =>
        {
            var eventWithId = await EventQueryBuilder.WithId(eventId).FirstOrDefaultAsync();

            if (eventWithId == null)
                throw new InvalidOperationException($"Event with id {eventId} is not found.");

            if (eventWithId.IsDeleted)
                throw new InvalidOperationException(
                    $"Event with id {eventId} was already deleted at {eventWithId.DeletedDateTime.ToString()}");

            return await EventQueryBuilder
                .DeleteAsync(eventWithId);
        });
    }

    public async Task<Result<EventRecord>> DeleteAsync(EventRecord eventRecord)
    {
        return await Result.Try(async () =>
        {
            if (eventRecord.IsDeleted)
                throw new InvalidOperationException(
                    $"Event with id {eventRecord.Id} was already deleted at {eventRecord.DeletedDateTime.ToString()}");

            return await EventQueryBuilder
                .DeleteAsync(eventRecord);
        });
    }

    public async Task<Result<EventRecord>> UpdateAsync(EventRecord eventRecord)
    {
        return await Result.Try(async () => { return await EventQueryBuilder.UpdateAsync(eventRecord); });
    }

    public Result CanCreate(PlaceRecord placeRecord, ProfileRecord userRecord)
    {
        return Result.Ok();
    }

    public Result CanView(EventRecord eventRecord, ProfileRecord? userRecord)
    {
        return Result.Ok();
    }

    public Result CanDelete(EventRecord eventRecord, ProfileRecord userRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail($"Event with id: {eventRecord.Id} was already deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь удалять события

        return Result.OkIf(eventRecord.OwnerId == userRecord.Id,
            "Only owner or moderator can delete this event");
    }

    public Result CanUpdate(EventRecord eventRecord, ProfileRecord userRecord)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail($"Event with id: {eventRecord.Id} was already deleted");

        // todo добавить проверку на роль. модератор тоже должен уметь удалять обновлять
        
        return Result.OkIf(eventRecord.OwnerId == userRecord.Id,
            "Only owner or moderator can update this event");
    }

    public Result CanViewAll(ProfileRecord? currentUser)
    {
        return Result.Ok();
    }
}