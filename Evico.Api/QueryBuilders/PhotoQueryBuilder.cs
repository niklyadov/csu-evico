using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PhotoQueryBuilder : QueryBuilder<PhotoRecord, ApplicationContext>
{
    public PhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}