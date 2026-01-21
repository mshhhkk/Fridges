using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fridges.Application.Interfaces;
using System.Threading.Tasks;
using Fridges.Application.DTOs;
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
    public async Task<ActionResult> GetFridgeInfo(Guid id)
    {
        var fridge = await _fridgeService.GetFridge(id);
        return Ok(fridge);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFridges()
    {
        var fridges = await _fridgeService.GetAllFridges();
        return Ok(fridges);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _fridgeService.DeleteFridge(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> AddFridge([FromBody] FridgeDto fridgeDto)
    {
        var addedFridge = await _fridgeService.AddFridge(fridgeDto);

        return CreatedAtAction(
            nameof(GetFridgeInfo),
            new { id = addedFridge.Id },
            addedFridge);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditFridgeInfo(Guid id, [FromBody] FridgeDto fridgeDto)
    {
        await _fridgeService.EditFridge(id, fridgeDto);
        return Ok();
    }
}
