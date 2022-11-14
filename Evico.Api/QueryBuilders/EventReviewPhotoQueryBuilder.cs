using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class EventReviewPhotoQueryBuilder : QueryBuilder<EventReviewPhotoRecord, ApplicationContext>
{
    public EventReviewPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}