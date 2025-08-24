using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsListAsync()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductInfoAsync(Guid id)
    {
        var product = await _service.GetAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDto dto)
    {
        var addedProduct = await _service.AddAsync(dto);
        return CreatedAtAction(
            nameof(GetProductInfoAsync),
            new { id = addedProduct.Id },
            addedProduct);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditProductInfoAsync(Guid id, [FromBody] EditProductDto dto)
    {
        await _service.EditAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/recipes")]
    public async Task<IActionResult> SearchRecipesByProductAsync(Guid id)
    {
        var recipe = await _service.GetRecipesByIdAsync(id);
        return Ok(recipe);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProductsByCategoryAsync([FromQuery] ProductCategory productCategory)
    {
        var products = await _service.GetAllByCategoryAsync(productCategory);
        return Ok(products);
    }
}
