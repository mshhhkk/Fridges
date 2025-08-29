using FluentValidation;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : Controller
{
    private readonly IRecipeService _recipeService;
    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipesAsync()
    {
        var result = await _recipeService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecipeInfoAsync(Guid id)
    {
        var result = await _recipeService.GetAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddRecipeAsync([FromBody] RecipeDto recipeDto, [FromServices] IValidator<RecipeDto> validation)
    {
        var validationResult = await validation.ValidateAsync(recipeDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _recipeService.AddAsync(recipeDto);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return CreatedAtAction(
            "GetRecipeInfo",
            new { id = result.Value.Id },
            result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecipeAsync(Guid id)
    {
        var result = await _recipeService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditRecipeAsync(Guid id, [FromBody] RecipeDto recipeDto, [FromServices] IValidator<RecipeDto> validation)
    {
        var validationResult = await validation.ValidateAsync(recipeDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _recipeService.EditAsync(id, recipeDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetRecipeProductsAsync(Guid id)
    {
        var result = await _recipeService.GetRecipeProductsByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
