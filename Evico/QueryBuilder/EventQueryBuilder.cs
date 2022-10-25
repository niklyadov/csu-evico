using Evico.Entity;

namespace Evico.QueryBuilder;

public class EventQueryBuilder : QueryBuilder<EventRecord, ApplicationContext>
{
    public EventQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}