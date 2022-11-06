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
    
    public async Task<Result<ProfileRecord>> AddAsync(ProfileRecord profile)
    {
        return await Result.Try(async () 
            => await _profileQueryBuilder
                .AddAsync(profile)
        );
    }

    public async Task<Result<ProfileRecord>> GetByIdAsync(long id)
    {
        return await Result.Try(async () 
            => await _profileQueryBuilder
                .WithId(id)
                .SingleAsync()
        );
    }
    
    public async Task<Result<ProfileRecord>> GetByUsernameAsync(String username)
    {
        return await Result.Try(async () 
            => await _profileQueryBuilder
                .WithUsername(username)
                .SingleAsync()
        );
    }
    
    public async Task<Result<ProfileRecord>> GetByVkIdAsync(long vkUserId)
    {
        return await Result.Try(async () 
            => await _profileQueryBuilder
                .WithVkId(vkUserId)
                .SingleAsync()
        );
    }

    public async Task<Result<ProfileRecord>> AddWithUsernameAsync(String username)
    {
        return await Result.Try(async () 
            => await _profileQueryBuilder
                .AddAsync(new ProfileRecord()
                {
                    Name = username
                })
        );
    }
}