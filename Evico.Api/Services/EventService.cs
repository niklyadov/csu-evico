using Evico.Api.Entity;
using Evico.Api.InputModels.Event;
using Evico.Api.QueryBuilder;
using FluentResults;

namespace Evico.Api.Services;

public class EventService
{
    private readonly ApplicationContext _context;

    public EventService(ApplicationContext context)
    {
        _context = context;
    }

    private EventQueryBuilder _eventQueryBuilder => new(_context);
    private PlaceQueryBuilder _placeQueryBuilder => new(_context);

    public async Task<Result<EventRecord>> AddAsync(EventRecord eventRecord)
    {
        // todo: add category to event
        return await Result.Try(async () =>
        {
            return await _eventQueryBuilder.AddAsync(eventRecord);
        });
    }

    public async Task<Result<List<EventRecord>>> GetAllAsync()
    {
        return await Result.Try(async () =>
        {
            return await _eventQueryBuilder
                .WhereNotDeleted()
                .ToListAsync();
        });
    }

    public async Task<Result<EventRecord>> GetByIdAsync(long eventId)
    {
        return await Result.Try(async () =>
        {
            return await _eventQueryBuilder
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
            var eventWithId = await _eventQueryBuilder.WithId(eventId).FirstOrDefaultAsync();

            if (eventWithId == null)
                throw new InvalidOperationException($"Event with id {eventId} is not found.");

            if (eventWithId.IsDeleted)
                throw new InvalidOperationException(
                    $"Event with id {eventId} was already deleted at {eventWithId.DeletedDateTime.ToString()}");

            return await _eventQueryBuilder
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

            return await _eventQueryBuilder
                .DeleteAsync(eventRecord);
        });
    }

    // todo: rewrite this
    public async Task<Result<EventRecord>> Update(UpdateEventInputModel updateEventModel)
    {
        // todo: add category to event
        return await Result.Try(async () =>
        {
            var eventRecord = await _eventQueryBuilder
                .WithId(updateEventModel.Id)
                .FirstOrDefaultAsync();

            if (eventRecord == null)
                throw new InvalidOperationException($"Event with id {updateEventModel.Id} is not found.");

            if (eventRecord.IsDeleted)
                throw new InvalidOperationException($"Event with id {updateEventModel.Id} is deleted");

            if (updateEventModel.Start != null)
                eventRecord.Start = updateEventModel.Start;

            if (updateEventModel.End != null)
                eventRecord.End = updateEventModel.End;

            if (updateEventModel.PlaceId != null)
            {
                var place = await _placeQueryBuilder
                    .WithId(updateEventModel.PlaceId.Value)
                    .FirstOrDefaultAsync();

                if (place == null)
                    throw new InvalidOperationException($"Place cannot be null. Place id: {updateEventModel.PlaceId}");

                if (place.IsDeleted)
                    throw new InvalidOperationException($"Place with id {place.Id} was deleted");

                eventRecord.Place = place;
            }

            if (!string.IsNullOrEmpty(updateEventModel.Name))
                eventRecord.Name = updateEventModel.Name;

            if (!string.IsNullOrEmpty(updateEventModel.Description))
                eventRecord.Description = updateEventModel.Description;

            return await _eventQueryBuilder.UpdateAsync(eventRecord);
        });
    }

    public Result CanCreate(PlaceRecord placeRecord, ProfileRecord userRecord)
    {
        return Result.Ok();
    }
    
    public Result CanView(EventRecord eventRecord, ProfileRecord userRecord)
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
}