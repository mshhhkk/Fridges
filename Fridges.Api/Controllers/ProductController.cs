using Fridges.Application;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsListAsync()
    {
        var result = await _productService.GetAllAsync();

        if (!result.IsSuccess)
            { return NotFound(result.Error); }

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductInfoAsync(Guid id)
    {
        var result= await _productService.GetAsync(id);

        if (!result.IsSuccess)
            { return NotFound(result.Error); }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDto dto)
    {
        var result = await _productService.AddAsync(dto);
        if (!result.IsSuccess)
        { return BadRequest(result.Error); }

        return CreatedAtAction(
            nameof(GetProductInfoAsync),
            new { id = result.Value.Id },
            result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditProductInfoAsync(Guid id, [FromBody] EditProductDto dto)
    {
        var result = await _productService.EditAsync(id, dto);
        if (!result.IsSuccess)
            { return BadRequest(result.Error); }

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result.IsSuccess)
            { return NotFound(result.Error); }

        return NoContent();
    }

    [HttpGet("{id:guid}/recipes")]
    public async Task<IActionResult> SearchRecipesByProductAsync(Guid id)
    {
        var result = await _productService.GetRecipesByIdAsync(id);
        if (!result.IsSuccess)
            { return BadRequest(result.Error); }

        return Ok(result.Value);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProductsByCategoryAsync([FromQuery] ProductCategory productCategory)
    {
        var result = await _productService.GetAllByCategoryAsync(productCategory);
        if (!result.IsSuccess)
            { return BadRequest(result.Error); }

        return Ok(result.Value);
    }
}
