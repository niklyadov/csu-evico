using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class ReviewQueryBuilder : QueryBuilder<EventReviewRecord, ApplicationContext>
{
    public ReviewQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}