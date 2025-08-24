using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
namespace Fridges.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetAsync(Guid id);
    Task<Product> AddAsync(ProductDto dto);
    Task EditAsync(Guid Id, EditProductDto dto);
    Task DeleteAsync(Guid id);
    Task<List<Recipe>> GetRecipesByIdAsync(Guid id);
    Task<List<Product>> GetAllByCategoryAsync(ProductCategory productCategory);
}
