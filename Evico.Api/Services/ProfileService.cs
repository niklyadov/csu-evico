using Evico.Api.Entity;
using Evico.Api.QueryBuilder;
using FluentResults;

namespace Evico.Api.Services;

public class ProfileService
{
    private readonly ProfileQueryBuilder _profileQueryBuilder;

    public ProfileService(ProfileQueryBuilder profileQueryBuilder)
    {
        _profileQueryBuilder = profileQueryBuilder;
    }

    public async Task<Result<ProfileRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () => { return await _profileQueryBuilder.WithId(id).SingleAsync(); });
    }
}