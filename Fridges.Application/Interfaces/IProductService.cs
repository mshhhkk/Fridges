using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
namespace Fridges.Application.Interfaces;

public interface IProductService
{
    Task<Result<List<Product>>> GetAllAsync();
    Task<Result<Product>> GetAsync(Guid id);
    Task<Result<Product>> AddAsync(ProductDto dto);
    Task<Result<Product>> EditAsync(Guid Id, EditProductDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<List<Recipe>>> GetRecipesByIdAsync(Guid id);
    Task<Result<List<Product>>> GetAllByCategoryAsync(ProductCategory productCategory);
}
