using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class ProfileQueryBuilder : QueryBuilder<ProfileRecord, ApplicationContext>
{
    public ProfileQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public ProfileQueryBuilder WithVkId(long vkUserId)
    {
        Query = Query.Where(x => x.VkUserId == vkUserId);

        return this;
    }

    public ProfileQueryBuilder WithUsername(string username)
    {
        Query = Query.Where(x => x.Name == username);

        return this;
    }
}