using Microsoft.AspNetCore.Mvc;
using Fridges.Application.Interfaces;
using Fridges.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Controllers;
using Fridges.Domain.Enums;
namespace Fridges.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductsList()
    {
        var products = await _productService.GetProductsList();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductInfo(Guid id)
    {
        var product = await _productService.GetProductInfo(id);
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
    {
        var addedProduct = await _productService.AddProduct(dto);
        return CreatedAtAction(
            nameof(GetProductInfo),
            new { id = addedProduct.Id },
            addedProduct);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditProductInfo(Guid id, [FromBody] EditProductDto dto)
    {
        await _productService.EditProductInfo(id,dto);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/recipes")]
    public async Task<IActionResult> SearchRecipesByProduct(Guid id)
    {
        var recipe = await _productService.SearchRecipesByProduct(id);
        return Ok(recipe);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProductsByCategory([FromQuery] ProductCategory productCategory)
    {
        var products = await _productService.SearchProductsByCategory(productCategory);
        return Ok(products);
    }
}
