using System.Globalization;
using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
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
    
    public async Task<Result<List<EventRecord>>> SearchAsync(EventSearchFilters filters)
    {
        return await Result.Try(async () =>
        {
            EventQueryBuilder eventsQueryBuilder = (EventQueryBuilder)EventQueryBuilder
                .Include(x => x.Categories)
                .Include(x => x.Participants)
                .WhereNotDeleted();

            eventsQueryBuilder = WithSearchQueryFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithPlaceId(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithStartDateFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithEndDateFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithOrganizersFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithInCategoriesFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = WithNotInCategoriesFilter(eventsQueryBuilder, filters);
            eventsQueryBuilder = eventsQueryBuilder.Sort(filters.SortBy, filters.SortOrder);
            eventsQueryBuilder = (EventQueryBuilder)eventsQueryBuilder.Skip(filters.Offset);
            eventsQueryBuilder = (EventQueryBuilder)eventsQueryBuilder.Limit(filters.Limit);
            
            return await eventsQueryBuilder.ToListAsync();
        });
    }

    private EventQueryBuilder WithSearchQueryFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (!String.IsNullOrEmpty(filters.SearchQuery))
        {
            return queryBuilder.SearchString(filters.SearchQuery);
        }
        
        return queryBuilder;
    }
    
    private EventQueryBuilder WithPlaceId(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (filters.PlaceId.HasValue)
        {
            return queryBuilder.WithPlaceId(filters.PlaceId.Value);
        }
        
        return queryBuilder;
    }
    
    private EventQueryBuilder WithStartDateFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (!String.IsNullOrEmpty(filters.StartDateBetweenA) 
            && !String.IsNullOrEmpty(filters.StartDateBetweenB))
        {
            var betweenA = DateTime.Parse(filters.StartDateBetweenA, CultureInfo.InvariantCulture);
            var betweenB = DateTime.Parse(filters.StartDateBetweenB, CultureInfo.InvariantCulture);

            return queryBuilder.WithStartDateBetween(betweenA, betweenB);
        }
        
        return queryBuilder;
    }
    
    private EventQueryBuilder WithEndDateFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (!String.IsNullOrEmpty(filters.EndDateBetweenA) 
            && !String.IsNullOrEmpty(filters.EndDateBetweenB))
        {
            var betweenA = DateTime.Parse(filters.EndDateBetweenA, CultureInfo.InvariantCulture);
            var betweenB = DateTime.Parse(filters.EndDateBetweenB, CultureInfo.InvariantCulture);

            return queryBuilder.WithEndDateBetween(betweenA, betweenB);
        }
        
        return queryBuilder;
    }

    private EventQueryBuilder WithOrganizersFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (!String.IsNullOrEmpty(filters.Organizers))
        {
            var withOrganizers = filters.Organizers.Split(',')
                .Select(x=> long.Parse(x)).ToList();

            return queryBuilder.WithOrganizers(withOrganizers);    
        }

        return queryBuilder;
    }

    private EventQueryBuilder WithInCategoriesFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
                    
        if (!String.IsNullOrEmpty(filters.InCategories))
        {
            var inCategories = filters.InCategories.Split(',')
                .Select(x=> long.Parse(x)).ToList();

           return queryBuilder.WhereCategoriesIn(inCategories);
        }

        return queryBuilder;
    }

    private EventQueryBuilder WithNotInCategoriesFilter(EventQueryBuilder queryBuilder, EventSearchFilters filters)
    {
        if (!String.IsNullOrEmpty(filters.NotInCategories))
        {
            var notInCategories = filters.NotInCategories.Split(',')
                .Select(x=> long.Parse(x)).ToList();

            return queryBuilder.WhereCategoriesNotIn(notInCategories);
        }

        return queryBuilder;
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

    public Result CanDelete(EventRecord eventRecord, ProfileRecord profile)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail($"Event with id: {eventRecord.Id} was already deleted");

        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(eventRecord.OwnerId == profile.Id,
            "Only owner or moderator can delete this event");
    }

    public Result CanUpdate(EventRecord eventRecord, ProfileRecord profile)
    {
        if (eventRecord.IsDeleted)
            return Result.Fail($"Event with id: {eventRecord.Id} was already deleted");

        if (profile.Role == UserRoles.Moderator)
            return Result.Ok();
        
        return Result.OkIf(eventRecord.OwnerId == profile.Id,
            "Only owner or moderator can update this event");
    }

    public Result CanViewAll(ProfileRecord? currentUser)
    {
        return Result.Ok();
    }
}