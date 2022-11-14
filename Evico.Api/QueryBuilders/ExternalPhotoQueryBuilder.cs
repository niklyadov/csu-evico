using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class ExternalPhotoQueryBuilder : QueryBuilder<PhotoRecord, ApplicationContext>
{
    public ExternalPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}