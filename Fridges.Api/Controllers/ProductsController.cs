using FluentValidation;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{id:guid}", Name = "GetProductById")]
    public async Task<IActionResult> GetProductInfoAsync(Guid id)
    {
        var result = await _productService.GetAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetProductsListAsync()
    {
        var result = await _productService.GetAllAsync();

        if (!result.IsSuccess)
        { return NotFound(result.Error); }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] AddProductDto dto, [FromServices] IValidator<AddProductDto> validation)
    {
        var validationResult = await validation.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _productService.AddAsync(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }
        var product = result.Value;

        return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditProductInfoAsync(Guid id, [FromBody] EditProductDto dto, [FromServices] IValidator<EditProductDto> validation)
    {
        var validationResult = await validation.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
        }

        var result = await _productService.EditAsync(id, dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return NoContent();
    }

    [HttpGet("{id:guid}/recipes")]
    public async Task<IActionResult> SearchRecipesByProductAsync(Guid id)
    {
        var result = await _productService.GetRecipesByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProductsByCategoryAsync([FromQuery] ProductCategory productCategory)
    {
        var result = await _productService.GetAllByCategoryAsync(productCategory);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
