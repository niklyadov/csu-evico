using Evico.Api.Entities;

namespace Evico.Api.QueryBuilders;

public class EventPhotoQueryBuilder : QueryBuilder<EventPhotoRecord, ApplicationContext>
{
    public EventPhotoQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}