using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : Controller
{
    private readonly IRecipeService _service;
    public RecipesController(IRecipeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipesAsync()
    {
        var recipes = await _service.GetAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecipeInfoAsync(Guid id)
    {
        var recipe = await _service.GetAsync(id);
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> AddRecipeAsync([FromBody] RecipeDto recipeDto)
    {
        var newRecipe = await _service.AddAsync(recipeDto);
        return CreatedAtAction(nameof(GetRecipeInfoAsync), new { id = newRecipe.Id }, newRecipe);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecipeAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditRecipeAsync(Guid id, [FromBody] RecipeDto recipeDto)
    {
        await _service.EditAsync(id, recipeDto);
        return Ok();
    }

    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetRecipeProductsAsync(Guid id)
    {
        var products = await _service.GetRecipeProductsByIdAsync(id);
        return Ok(products);
    }
}
