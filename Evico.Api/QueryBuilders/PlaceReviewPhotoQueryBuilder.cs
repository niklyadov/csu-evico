using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class PlaceReviewPhotoQueryBuilder : QueryBuilder<PlaceReviewPhotoRecord, ApplicationContext>
{
    public PlaceReviewPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}