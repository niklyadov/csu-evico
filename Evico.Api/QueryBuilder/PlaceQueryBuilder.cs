using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class PlaceQueryBuilder : QueryBuilder<PlaceRecord, ApplicationContext>
{
    public PlaceQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}