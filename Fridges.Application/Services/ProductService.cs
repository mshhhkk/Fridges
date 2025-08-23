using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Persistance.Interfaces;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;

namespace Fridges.Application.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IRecipeRepository _recipeRepository;

    public ProductService(IProductRepository productRepository, IRecipeRepository recipeRepository)
    {
        _productRepository = productRepository;
        _recipeRepository = recipeRepository;
    }
    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products;
    }
    public async Task<Product> GetAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product;
    }
    public async Task<Product> AddAsync(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            FridgeId = dto.FridgeId,
            Release = dto.Release,
            Expiration = dto.Expiration,
            Weight = dto.Weight,
            IsFresh = dto.IsFresh,
            ProductTypeId = dto.ProductTypeId
        };
        await _productRepository.AddAsync(product);
        return product;
    }

    public async Task EditAsync(Guid id,EditProductDto dto)
    {
        
        var product = await _productRepository.GetByIdAsync(id);
        product.FridgeId = dto.FridgeId;
        product.Release = dto.Release;
        product.Expiration = dto.Expiration;
        product.Weight = dto.Weight;
        product.IsFresh = dto.IsFresh;
        await _productRepository.UpdateAsync(product);


    }
    public async Task DeleteAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);

    }
    public async Task<List<Recipe>> SearchRecipesByIdAsync(Guid id)
    {
        var productType = await _productRepository.GetProductTypeIdAsync(id);
        var recipes = await _recipeRepository.SearchRecipesByProductTypeId(productType);
        return recipes;
    }
    public async Task<List<Product>> SearchProductsByCategoryAsync(ProductCategory productCategory)
    {
        var products = await _productRepository.GetAllByCategoryAsync(productCategory);
        return products;
    }
}
