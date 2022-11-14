using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
using Evico.Api.UseCases.Event.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class EventCategoryController : BaseController
{
    private readonly AddEventCategoryUseCase _addEventCategoryUseCase;
    private readonly DeleteEventCategoryUseCase _deleteEventCategoryUseCase;
    private readonly GetEventCategoriesUseCase _getEventCategoriesUseCase;
    private readonly GetEventCategoryByIdUseCase _getEventCategoryByIdUseCase;
    private readonly UpdateEventCategoryUseCase _updateEventCategoryUseCase;

    public EventCategoryController(IServiceProvider services)
    {
        _addEventCategoryUseCase = services.GetRequiredService<AddEventCategoryUseCase>();
        _getEventCategoryByIdUseCase = services.GetRequiredService<GetEventCategoryByIdUseCase>();
        _getEventCategoriesUseCase = services.GetRequiredService<GetEventCategoriesUseCase>();
        _updateEventCategoryUseCase = services.GetRequiredService<UpdateEventCategoryUseCase>();
        _deleteEventCategoryUseCase = services.GetRequiredService<DeleteEventCategoryUseCase>();
    }

    [HttpPost]
    public async Task<ActionResult<EventCategoryRecord>> Add([FromBody] AddEventCategoryInputModel inputModel)
    {
        return await _addEventCategoryUseCase.AddAsync(inputModel, User);
    }

    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<EventCategoryRecord>> GetById(long categoryId)
    {
        return await _getEventCategoryByIdUseCase.GetByIdAsync(categoryId, User);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<EventCategoryRecord>>> GetAll()
    {
        return await _getEventCategoriesUseCase.GetAllAsync(User);
    }

    [HttpPut]
    public async Task<ActionResult<List<EventCategoryRecord>>> Update(
        [FromBody] UpdateEventCategoryInputModel inputModel)
    {
        return await _updateEventCategoryUseCase.UpdateAsync(inputModel, User);
    }

    [HttpDelete("{categoryId}")]
    public async Task<ActionResult<List<EventCategoryRecord>>> Delete(long categoryId)
    {
        return await _deleteEventCategoryUseCase.DeleteAsync(categoryId, User);
    }
}