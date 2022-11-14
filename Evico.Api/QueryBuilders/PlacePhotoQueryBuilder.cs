using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PlacePhotoQueryBuilder : QueryBuilder<PlacePhotoRecord, ApplicationContext>
{
    public PlacePhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}