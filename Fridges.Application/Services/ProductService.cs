using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
using Fridges.Persistance.Interfaces;

namespace Fridges.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IFridgeRepository _fridgeRepository;

    public ProductService(IProductRepository productRepository, IRecipeRepository recipeRepository, IFridgeRepository fridgeRepository)
    {
        _productRepository = productRepository;
        _recipeRepository = recipeRepository;
        _fridgeRepository = fridgeRepository;
    }

    public async Task<Result<List<Product>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        if (products == null)
        {
            return Result<List<Product>>.Failure("No products found");
        }

        return Result<List<Product>>.Success(products);
    }

    public async Task<Result<Product>> GetAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result<Product>.Failure($"No product with {id} found");
        }

        return Result<Product>.Success(product);
    }

    public async Task<Result<Product>> AddAsync(AddProductDto dto)
    {
        var fridge = await _fridgeRepository.GetByIdAsync(Guid.Parse(dto.FridgeId));
        if (fridge == null)
        {
            return Result<Product>.Failure("Fridge not found");
        }

        var parsedFridgeId = Guid.Parse(dto.FridgeId);
        var productAmount = await _fridgeRepository.GetCurrentProductsAmountByIdAsync(parsedFridgeId);
        if (productAmount >= fridge.Capacity)
        {
            return Result<Product>.Failure("Fridge is full");
        }

        var parsedExpiration = DateOnly.Parse(dto.Expiration);
        if (parsedExpiration <= DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return Result<Product>.Failure("Expiration date should be in the future.");
        }

        var parsedRelease = DateOnly.Parse(dto.Release);
        if (parsedRelease >= parsedExpiration)
        {
            return Result<Product>.Failure("Expiration should be later than release");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            FridgeId = parsedFridgeId,
            Release = parsedRelease,
            Expiration = parsedExpiration,
            Weight = dto.Weight,
            IsFresh = dto.IsFresh,
            ProductTypeId = dto.ProductTypeId
        };

        await _productRepository.AddAsync(product);
        return Result<Product>.Success(product);
    }

    public async Task<Result<Product>> EditAsync(Guid id, EditProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result<Product>.Failure($"No product with {id} found");
        }

        var parsedFridgeId = Guid.Parse(dto.FridgeId);
        var fridge = await _fridgeRepository.GetByIdAsync(parsedFridgeId);

        if (fridge == null)
        {
            return Result<Product>.Failure("Fridge not found");
        }

        var productAmount = await _fridgeRepository.GetCurrentProductsAmountByIdAsync(parsedFridgeId);
        if (productAmount >= fridge.Capacity)
        {
            return Result<Product>.Failure("Fridge is full");
        }

        product.FridgeId = parsedFridgeId;
        product.Weight = dto.Weight;
        product.IsFresh = dto.IsFresh;
        await _productRepository.UpdateAsync(product);

        return Result<Product>.Success(product);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result.Failure($"No product with {id} found");
        }

        await _productRepository.DeleteAsync(id);

        return Result.Success();
    }

    public async Task<Result<List<Recipe>>> GetRecipesByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result<List<Recipe>>.Failure($"No product with {id} found");
        }

        var productType = await _productRepository.GetProductTypeIdAsync(id);

        var recipes = await _recipeRepository.GetAllByProductTypeIdAsync(productType);

        return Result<List<Recipe>>.Success(recipes);
    }

    public async Task<Result<List<Product>>> GetAllByCategoryAsync(ProductCategory productCategory)
    {
        if (!Enum.IsDefined(typeof(ProductCategory), productCategory))
        {
            return Result<List<Product>>.Failure("Invalid product category");
        }

        var products = await _productRepository.GetAllByCategoryAsync(productCategory);

        return Result<List<Product>>.Success(products);
    }
}
