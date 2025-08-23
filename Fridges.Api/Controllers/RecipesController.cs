using Microsoft.AspNetCore.Mvc;
using Fridges.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Fridges.Application.DTOs;
namespace Fridges.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RecipesController:Controller
{
    private readonly IRecipeService _service;
    public RecipesController(IRecipeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipes = await _service.GetAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecipeInfo(Guid id)
    {
        var recipe = await _service.GetAsync(id);
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] RecipeDto recipeDto)
    {
        var newRecipe = await _service.AddAsync(recipeDto);
        return CreatedAtAction(nameof(GetRecipeInfo), new { id = newRecipe.Id }, newRecipe);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditRecipe(Guid id, [FromBody] RecipeDto recipeDto)
    {
        await _service.EditAsync(id, recipeDto);
        return Ok();
    }

    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetRecipeProducts(Guid id)
    {
        var products = await _service.GetRecipeProductsAsync(id);
        return Ok(products); 
    }
}
