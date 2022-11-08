using Evico.Api.Entities;
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
    public async Task<ActionResult<PlaceCategoryRecord>> Add(AddPlaceCategoryInputModel inputModel)
    {
        return await _addPlaceCategoryUseCase.AddAsync(inputModel, User);
    }
    
    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<PlaceCategoryRecord>> GetById(long categoryId)
    {
        return await _getPlaceCategoryByIdUseCase.GetByIdAsync(categoryId, User);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PlaceCategoryRecord>>> GetAll()
    {
        return await _getPlaceCategoriesUseCase.GetAllAsync(User);
    }
    
    [HttpPut]
    public async Task<ActionResult<List<PlaceCategoryRecord>>> Update(UpdatePlaceCategoryInputModel inputModel)
    {
        return await _updatePlaceCategoryUseCase.UpdateAsync(inputModel, User);
    }
    
    [HttpDelete("{categoryId}")]
    public async Task<ActionResult<List<PlaceCategoryRecord>>> Delete(long categoryId)
    {
        return await _deletePlaceCategoryUseCase.DeleteAsync(categoryId, User);
    }
}