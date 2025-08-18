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
    public async Task<List<Product>> GetProductsList()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products;
    }
    public async Task<Product> GetProductInfo(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product;
    }
    public async Task<Product> AddProduct(ProductDto dto)
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
        await _productRepository.AddProductAsync(product);
        return product;
    }

    public async Task EditProductInfo(Guid id,EditProductDto dto)
    {
        
        var product = await _productRepository.GetProductByIdAsync(id);
        product.FridgeId = dto.FridgeId;
        product.Release = dto.Release;
        product.Expiration = dto.Expiration;
        product.Weight = dto.Weight;
        product.IsFresh = dto.IsFresh;
        await _productRepository.UpdateProductAsync(product);


    }
    public async Task DeleteProduct(Guid id)
    {
        await _productRepository.DeleteProductAsync(id);

    }
    public async Task<List<Recipe>> SearchRecipesByProduct(Guid id)
    {
        var productType = await _productRepository.GetProductTypeIdAsync(id);
        var recipes = await _recipeRepository.SearchRecipesByProductTypeId(productType);
        return recipes;
    }
    public async Task<List<Product>> SearchProductsByCategory(ProductCategory productCategory)
    {
        var products = await _productRepository.SearchProductsByCategoryAsync(productCategory);
        return products;
    }
}
