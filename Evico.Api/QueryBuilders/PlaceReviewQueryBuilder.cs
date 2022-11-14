using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PlaceReviewQueryBuilder : QueryBuilder<PlaceReviewRecord, ApplicationContext>
{
    public PlaceReviewQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}