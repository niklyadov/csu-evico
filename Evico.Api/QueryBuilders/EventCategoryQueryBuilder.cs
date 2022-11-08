using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class EventCategoryQueryBuilder : QueryBuilder<EventCategoryRecord, ApplicationContext>
{
    public EventCategoryQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}