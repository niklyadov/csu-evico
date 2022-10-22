using Evico.Entity;

namespace Evico.QueryBuilder;

public class PlaceQueryBuilder : QueryBuilder<PlaceRecord, ApplicationContext>
{
    public PlaceQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}