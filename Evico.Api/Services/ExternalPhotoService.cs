using Evico.Api.QueryBuilder;

namespace Evico.Api.Services;

public class ExternalPhotoService
{
    private readonly ExternalPhotoQueryBuilder _externalPhotoQueryBuilder;

    public ExternalPhotoService(ExternalPhotoQueryBuilder externalPhotoQueryBuilder)
    {
        _externalPhotoQueryBuilder = externalPhotoQueryBuilder;
    }
}