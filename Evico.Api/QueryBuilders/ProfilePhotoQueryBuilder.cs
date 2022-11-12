using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class ProfilePhotoQueryBuilder : QueryBuilder<ProfilePhotoRecord, ApplicationContext>
{
    public ProfilePhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}