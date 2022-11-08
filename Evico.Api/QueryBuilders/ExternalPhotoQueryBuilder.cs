using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class ExternalPhotoQueryBuilder : QueryBuilder<ExternalPhotoRecord, ApplicationContext>
{
    public ExternalPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}