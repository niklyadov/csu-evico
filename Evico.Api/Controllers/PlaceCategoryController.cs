using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Evico.Api.UseCases.Place.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class PlaceCategoryController : BaseController
{
    private readonly AddPlaceCategoryUseCase _addPlaceCategoryUseCase;
    private readonly GetPlaceCategoryByIdUseCase _getPlaceCategoryByIdUseCase;
    private readonly GetPlaceCategoriesUseCase _getPlaceCategoriesUseCase;
    private readonly UpdatePlaceCategoryUseCase _updatePlaceCategoryUseCase;
    private readonly DeletePlaceCategoryUseCase _deletePlaceCategoryUseCase;
        
    public PlaceCategoryController(IServiceProvider services)
    {
        _addPlaceCategoryUseCase = services.GetRequiredService<AddPlaceCategoryUseCase>();
        _getPlaceCategoryByIdUseCase = services.GetRequiredService<GetPlaceCategoryByIdUseCase>();
        _getPlaceCategoriesUseCase = services.GetRequiredService<GetPlaceCategoriesUseCase>();
        _updatePlaceCategoryUseCase = services.GetRequiredService<UpdatePlaceCategoryUseCase>();
        _deletePlaceCategoryUseCase = services.GetRequiredService<DeletePlaceCategoryUseCase>();
    }
    [HttpPost]
    public ActionResult<PlaceCategoryRecord> Add(AddPlaceCategoryInputModel inputModel)
    {
        return _addPlaceCategoryUseCase.AddAsync(inputModel, User);
    }
    
    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public ActionResult<PlaceCategoryRecord> GetById(long categoryId)
    {
        return _getPlaceCategoryByIdUseCase.GetByIdAsync(categoryId, User);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<PlaceCategoryRecord>> GetAll()
    {
        return _getPlaceCategoriesUseCase.GetAllAsync(User);
    }
    
    [HttpPut]
    public ActionResult<List<PlaceCategoryRecord>> Update(UpdatePlaceCategoryInputModel inputModel)
    {
        return _updatePlaceCategoryUseCase.UpdateAsync(inputModel, User);
    }
    
    [HttpDelete("{categoryId}")]
    public ActionResult<List<PlaceCategoryRecord>> Delete(long categoryId)
    {
        return _deletePlaceCategoryUseCase.DeleteAsync(categoryId, User);
    }
}