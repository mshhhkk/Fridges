using Microsoft.AspNetCore.Mvc;
using Fridges.Application.Interfaces;
using Fridges.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Controllers;
using Fridges.Domain.Enums;
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
    public async Task<IActionResult> ProductsList()
    {
        var products = await _service.GetProductsList();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductInfo(Guid id)
    {
        var product = await _service.GetProductInfo(id);
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
    {
        var addedProduct = await _service.AddProduct(dto);
        return CreatedAtAction(
            nameof(GetProductInfo),
            new { id = addedProduct.Id },
            addedProduct);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditProductInfo(Guid id, [FromBody] EditProductDto dto)
    {
        await _service.EditProductInfo(id,dto);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _service.DeleteProduct(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/recipes")]
    public async Task<IActionResult> SearchRecipesByProduct(Guid id)
    {
        var recipe = await _service.SearchRecipesByProduct(id);
        return Ok(recipe);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProductsByCategory([FromQuery] ProductCategory productCategory)
    {
        var products = await _service.SearchProductsByCategory(productCategory);
        return Ok(products);
    }

     


}
