using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PlaceQueryBuilder : QueryBuilder<PlaceRecord, ApplicationContext>
{
    public PlaceQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}