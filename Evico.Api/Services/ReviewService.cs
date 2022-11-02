using Evico.Api.Entity;
using Evico.Api.InputModels;
using Evico.Api.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Services;

public class ReviewService
{
    private readonly ApplicationContext _context;

    public ReviewService(ApplicationContext context)
    {
        _context = context;
    }

    private EventQueryBuilder _eventQueryBuilder => new(_context);
    private ProfileQueryBuilder _profileQueryBuilder => new(_context);
    private ReviewQueryBuilder _reviewQueryBuilder => new(_context);

    public async Task<IActionResult> Add(ReviewInputModel reviewInputModel)
    {
        try
        {
            var eventRecord = await _eventQueryBuilder.WithId(reviewInputModel.EventId).FirstOrDefaultAsync();
            if (eventRecord == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {reviewInputModel.EventId}");

            var authorRecord = await _profileQueryBuilder.WithId(reviewInputModel.AuthorId).FirstOrDefaultAsync();
            if (authorRecord == null)
                throw new InvalidOperationException($"Event cannot be null. Event id: {reviewInputModel.AuthorId}");

            if (reviewInputModel.Rate < 1 || reviewInputModel.Rate > 5)
                throw new InvalidOperationException($"Rate should be from 1 to 5");

            // todo Добавить фото
            var review = new EventReviewRecord
            {
                Event = eventRecord,
                Author = authorRecord,
                Rate = reviewInputModel.Rate,
                Comment = reviewInputModel.Comment,
                Photos = new()
            };

            var result = await _reviewQueryBuilder.AddAsync(review);

            return new OkObjectResult(result);
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(exception.ToString());
        }
    }
}