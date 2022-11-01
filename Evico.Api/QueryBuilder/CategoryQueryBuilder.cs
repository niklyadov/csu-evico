using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class CategoryQueryBuilder : QueryBuilder<EventCategoryRecord, ApplicationContext>
{
    public CategoryQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}