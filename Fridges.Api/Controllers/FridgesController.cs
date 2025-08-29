using FluentValidation;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FridgesController : Controller
{
    private readonly IFridgeService _fridgeService;
    public FridgesController(IFridgeService fridgeService)
    {
        _fridgeService = fridgeService;
    }

    [HttpGet("{id:guid}", Name = "GetFridgeById")]
    public async Task<ActionResult> GetFridgeInfoAsync(Guid id)
    {
        var result = await _fridgeService.GetAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllFridgesAsync()
    {
        var result = await _fridgeService.GetAllAsync();

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFridgeAsync(Guid id)
    {
        var result = await _fridgeService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> AddFridgeAsync([FromBody] FridgeDto fridgeDto, [FromServices] IValidator<FridgeDto> validator)
    {
        var validationResult = await validator.ValidateAsync(fridgeDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _fridgeService.AddAsync(fridgeDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            "GetFridgeInfo",
            new { id = result.Value.Id },
             result.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditFridgeInfoAsync(Guid id, [FromBody] FridgeDto fridgeDto, [FromServices] IValidator<FridgeDto> validator)
    {
        var validationResult = await validator.ValidateAsync(fridgeDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _fridgeService.EditAsync(id, fridgeDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
