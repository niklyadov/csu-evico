using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class PlaceReviewQueryBuilder : QueryBuilder<PlaceReviewRecord, ApplicationContext>
{
    public PlaceReviewQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}