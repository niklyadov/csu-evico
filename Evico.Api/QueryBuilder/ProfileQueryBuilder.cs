using Evico.Api.Entity;

namespace Evico.Api.QueryBuilder;

public class ProfileQueryBuilder : QueryBuilder<ProfileRecord, ApplicationContext>
{
    public ProfileQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}