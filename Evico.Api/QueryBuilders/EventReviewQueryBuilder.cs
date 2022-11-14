using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class EventReviewQueryBuilder : QueryBuilder<EventReviewRecord, ApplicationContext>
{
    public EventReviewQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}