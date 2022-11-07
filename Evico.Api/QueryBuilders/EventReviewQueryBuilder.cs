using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class EventReviewQueryBuilder : QueryBuilder<EventReviewRecord, ApplicationContext>
{
    public EventReviewQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}