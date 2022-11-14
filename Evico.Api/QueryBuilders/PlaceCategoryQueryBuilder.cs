using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PlaceCategoryQueryBuilder : QueryBuilder<PlaceCategoryRecord, ApplicationContext>
{
    public PlaceCategoryQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}