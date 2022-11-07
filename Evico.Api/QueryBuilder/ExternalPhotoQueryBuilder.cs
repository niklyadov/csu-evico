using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class ExternalPhotoQueryBuilder : QueryBuilder<ExternalPhotoRecord, ApplicationContext>
{
    public ExternalPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}