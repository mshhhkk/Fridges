using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Recipe;

namespace Fridges.Application.Interfaces;

public interface IRecipeService
{
    Task<Result<List<Recipe>>> GetAllAsync();
    Task<Result<Recipe>> GetAsync(Guid id);
    Task<Result<Recipe>> AddAsync(RecipeDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<Recipe>> EditAsync(Guid id, RecipeDto dto);
    Task<Result<List<RecipeProduct>>> GetRecipeProductsByIdAsync(Guid id);
}
