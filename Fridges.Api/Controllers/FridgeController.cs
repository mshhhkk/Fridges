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

    [HttpGet("{id}")]
    public async Task<ActionResult> GetFridgeInfoAsync(Guid id)
    {
        var fridge = await _fridgeService.GetAsync(id);
        return Ok(fridge);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFridgesAsync()
    {
        var fridges = await _fridgeService.GetAllAsync();
        return Ok(fridges);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFridgeAsync(Guid id)
    {
        await _fridgeService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> AddFridgeAsync([FromBody] FridgeDto fridgeDto)
    {
        var addedFridge = await _fridgeService.AddAsync(fridgeDto);

        return CreatedAtAction(
            nameof(GetFridgeInfoAsync),
            new { id = addedFridge.Id },
            addedFridge);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditFridgeInfoAsync(Guid id, [FromBody] FridgeDto fridgeDto)
    {
        await _fridgeService.EditAsync(id, fridgeDto);
        return Ok();
    }
}
