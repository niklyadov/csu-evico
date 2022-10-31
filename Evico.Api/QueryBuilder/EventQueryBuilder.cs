using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class EventQueryBuilder : QueryBuilder<EventRecord, ApplicationContext>
{
    public EventQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}