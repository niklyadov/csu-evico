using Evico.Api.Entities;
using Evico.Api.Enums;
using Evico.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Evico.Api.QueryBuilders;

public class PlaceQueryBuilder : QueryBuilder<PlaceRecord, ApplicationContext>
{
    public PlaceQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public PlaceQueryBuilder WhereCategoriesIn(List<long> categoryIds)
    {
        foreach (var categoryId in categoryIds.Distinct())
            Query = Query.Where(e 
                => e.Categories.Select(cat => cat.Id)
                    .Contains(categoryId));

        return this;
    }
    
    public PlaceQueryBuilder WhereCategoriesNotIn(List<long> categoryIds)
    {
        foreach (var categoryId in categoryIds.Distinct())
            Query = Query.Where(e 
                => ! e.Categories.Select(cat => cat.Id)
                    .Contains(categoryId));

        return this;
    }

    public PlaceQueryBuilder Sort(PlaceSearchSortType sortBy, SearchSortOrderType orderBy)
    {
        var desc = orderBy == SearchSortOrderType.Desc;
        switch (sortBy)
        {
            default:
            case PlaceSearchSortType.Id:
                Query = Query.SortBy(e => e.Id, desc);
                break;
            case PlaceSearchSortType.Name:
                Query = Query.SortBy(e => e.Name, desc);
                break;
        }

        return this;
    }

    public PlaceQueryBuilder SearchString(String searchString)
    {
        Query = Query.Where(e => 
            EF.Functions.Like(e.Name, $"%{searchString}%") 
            || EF.Functions.Like(e.Description, $"%{searchString}%"));
        
        return this;
    }
    
    public PlaceQueryBuilder WithOwnerId(long filtersOwnerId)
    {
        Query = Query.Where(e => e.OwnerId == filtersOwnerId);
        
        return this;
    }
}