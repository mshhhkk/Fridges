using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities;
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

        if(products==null)
        {
            return Result<List<Product>>.Failure("No products found");
        }
        else
        {
            return Result<List<Product>>.Success(products);
        }
    }

    public async Task<Result<Product>> GetAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result<Product>.Failure($"No product with {id} found");
        }
        else
        {
            return Result<Product>.Success(product);
        }
    }

    public async Task<Result<Product>> AddAsync(ProductDto dto)
    {
        if (dto.ProductTypeId <=0)
        {
            return Result<Product>.Failure("The productTypeId is required");
        }

        if (dto.FridgeId==Guid.Empty)
            { return Result<Product>.Failure("Product should be in the fridge"); }
        var fridge = await _fridgeRepository.GetByIdAsync(dto.FridgeId);

        if(fridge == null)
            { return Result<Product>.Failure("Fridge not found"); }

        var productAmount = await _fridgeRepository.GetCurrentProductsAmountByIdAsync(dto.FridgeId);
        if(productAmount>=fridge.Capacity)
        { return Result<Product>.Failure("Fridge is full"); }

        if (dto.Weight <= 0)
            { return Result<Product>.Failure("Weight should be more than 0"); }

        if (dto.Expiration <= DateOnly.FromDateTime(DateTime.UtcNow))
            { return Result<Product>.Failure("Expiration date should be in the future."); }

        if (dto.Release >= dto.Expiration)
            { return Result<Product>.Failure("Expiration should be later than release"); }

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
        return Result<Product>.Success(product);
    }

    public async Task<Result<Product>> EditAsync(Guid id, EditProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Result<Product>.Failure($"No product with {id} found");
        }
        
        if (dto.FridgeId == Guid.Empty)
            { return Result<Product>.Failure("Product should be in the fridge"); }
        var fridge = await _fridgeRepository.GetByIdAsync(dto.FridgeId);

        if (fridge == null)
        { return Result<Product>.Failure("Fridge not found"); }

        var productAmount = await _fridgeRepository.GetCurrentProductsAmountByIdAsync(dto.FridgeId);
        if (productAmount >= fridge.Capacity)
        { return Result<Product>.Failure("Fridge is full"); }

        if (dto.Weight <= 0)
        { return Result<Product>.Failure("Weight should be more than 0"); }

        if (dto.Expiration <= DateOnly.FromDateTime(DateTime.UtcNow))
        { return Result<Product>.Failure("Expiration date should be in the future."); }

        if (dto.Release >= dto.Expiration)
        { return Result<Product>.Failure("Expiration should be later than release"); }

        product.FridgeId = dto.FridgeId;
        product.Release = dto.Release;
        product.Expiration = dto.Expiration;
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
