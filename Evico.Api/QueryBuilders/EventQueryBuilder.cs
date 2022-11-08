using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class EventQueryBuilder : QueryBuilder<EventRecord, ApplicationContext>
{
    public EventQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}