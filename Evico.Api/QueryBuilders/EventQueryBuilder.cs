using Evico.Api.Entities;
using Evico.Api.Enums;
using Evico.Api.Extensions;

namespace Evico.Api.QueryBuilders;

public class EventQueryBuilder : QueryBuilder<EventRecord, ApplicationContext>
{
    public EventQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public EventQueryBuilder WithOrganizers(List<long> organizerIds)
    {
        foreach (var organizerId in organizerIds.Distinct())
            Query = Query.Where(e 
                => e.Organizers.Select(o => o.Id)
                    .Contains(organizerId));

        return this;
    }
    
    public EventQueryBuilder WhereCategoriesIn(List<long> categoryIds)
    {
        foreach (var categoryId in categoryIds.Distinct())
            Query = Query.Where(e 
                => e.Categories.Select(cat => cat.Id)
                    .Contains(categoryId));

        return this;
    }
    
    public EventQueryBuilder WhereCategoriesNotIn(List<long> categoryIds)
    {
        foreach (var categoryId in categoryIds.Distinct())
            Query = Query.Where(e 
                => ! e.Categories.Select(cat => cat.Id)
                    .Contains(categoryId));

        return this;
    }

    public EventQueryBuilder Sort(EventSearchSortType sortBy, SearchSortOrderType orderBy)
    {
        var desc = orderBy == SearchSortOrderType.Desc;
        switch (sortBy)
        {
            default:
            case EventSearchSortType.Id:
                Query = Query.SortBy(e => e.Id, desc);
                break;
            case EventSearchSortType.Name:
                Query = Query.SortBy(e => e.Name, desc);
                break;
            // todo: avg rate
            //case EventSearchSortType.AvgRate:
            //    Query = Query.OrderBy(e => e.Id, orderBy == SearchSortOrderType.Desc);
            //    break;
            case EventSearchSortType.StartDate:
                Query = Query.SortBy(e => e.Start, desc);
                break;
            case EventSearchSortType.EndDate:
                Query = Query.SortBy(e => e.End, desc);
                break;
            // todo: Participants Count
            case EventSearchSortType.ParticipantsCount:
                Query = Query.SortBy(e => e.Participants.Count, desc);
                break;
            // todo: Subscribers count
            //case EventSearchSortType.SubscribersCount:
            //    Query = Query.OrderBy(e => e.Subscribers.Count, desc);
            //    break;
        }

        return this;
    }

    public EventQueryBuilder SearchString(String searchString)
    {
        Query = Query.Where(e => SearchString(e, searchString));
        
        return this;
    }
    
    public EventQueryBuilder WithPlaceId(long placeId)
    {
        Query = Query.Where(e => e.PlaceId == placeId);
        
        return this;
    }
    
    public EventQueryBuilder WithStartDateBetween(DateTime betweenA, DateTime betweenB)
    {
        Query = Query.Where(e=> e.Start.HasValue)
            .Where(e => e.Start!.IsBetween<DateTime>(betweenA, betweenB));
        
        return this;
    }
    
    public EventQueryBuilder WithEndDateBetween(DateTime betweenA, DateTime betweenB)
    {
        Query = Query.Where(e=> e.Start.HasValue)
            .Where(e => e.End!.IsBetween<DateTime>(betweenA, betweenB));
        
        return this;
    }

    private bool SearchString(EventRecord eventRecord, String searchString)
        => eventRecord.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
           || eventRecord.Description.Contains(searchString, StringComparison.InvariantCultureIgnoreCase);
}