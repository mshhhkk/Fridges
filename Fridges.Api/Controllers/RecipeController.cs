using Microsoft.AspNetCore.Mvc;
using Fridges.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Fridges.Application.DTOs;
namespace Fridges.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RecipeController:Controller
{
    private readonly IRecipeService _service;
    public RecipeController(IRecipeService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipes = await _service.GetAllRecipes();
        return Ok(recipes);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecipeInfo(Guid id)
    {
        var recipe = await _service.GetRecipeInfo(id);
        return Ok(recipe);
    }
    [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] RecipeDto recipeDto)
    {
        var newRecipe = await _service.AddRecipe(recipeDto);
        return CreatedAtAction(nameof(GetRecipeInfo), new { id = newRecipe.Id }, newRecipe);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        await _service.DeleteRecipe(id);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditRecipe(Guid id, [FromBody] RecipeDto recipeDto)
    {
        await _service.EditRecipe(id, recipeDto);
        return Ok();
    }
    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetRecipeProducts(Guid id)
    {
        var products = await _service.GetRecipeProducts(id);
        return Ok(products); 
    }

    [HttpPost]
    public async Task<IActionResult> AddProductInRecipe(Guid id, [FromBody] RecipeProductDto dto)
    {
        var newRecipeProduct = await _service.AddProductInRecipe(id, dto);
        return CreatedAtAction()

    }




}
