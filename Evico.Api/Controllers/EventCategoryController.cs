using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
using Evico.Api.UseCases.Event.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class EventCategoryController : BaseController
{
    private readonly AddEventCategoryUseCase _addEventCategoryUseCase;
    private readonly GetEventCategoryByIdUseCase _getEventCategoryByIdUseCase;
    private readonly GetEventCategoriesUseCase _getEventCategoriesUseCase;
    private readonly UpdateEventCategoryUseCase _updateEventCategoryUseCase;
    private readonly DeleteEventCategoryUseCase _deleteEventCategoryUseCase;
        
    public EventCategoryController(IServiceProvider services)
    {
        _addEventCategoryUseCase = services.GetRequiredService<AddEventCategoryUseCase>();
        _getEventCategoryByIdUseCase = services.GetRequiredService<GetEventCategoryByIdUseCase>();
        _getEventCategoriesUseCase = services.GetRequiredService<GetEventCategoriesUseCase>();
        _updateEventCategoryUseCase = services.GetRequiredService<UpdateEventCategoryUseCase>();
        _deleteEventCategoryUseCase = services.GetRequiredService<DeleteEventCategoryUseCase>();
    }

    [HttpPost]
    public ActionResult<EventCategoryRecord> Add(AddEventCategoryInputModel inputModel)
    {
        return _addEventCategoryUseCase.AddAsync(inputModel, User);
    }
    
    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public ActionResult<EventCategoryRecord> GetById(long categoryId)
    {
        return _getEventCategoryByIdUseCase.GetByIdAsync(categoryId, User);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<EventCategoryRecord>> GetAll()
    {
        return _getEventCategoriesUseCase.GetAllAsync(User);
    }
    
    [HttpPut]
    public ActionResult<List<EventCategoryRecord>> Update(UpdateEventCategoryInputModel inputModel)
    {
        return _updateEventCategoryUseCase.UpdateAsync(inputModel, User);
    }
    
    [HttpDelete("{categoryId}")]
    public ActionResult<List<EventCategoryRecord>> Delete(long categoryId)
    {
        return _deleteEventCategoryUseCase.DeleteAsync(categoryId, User);
    }
}