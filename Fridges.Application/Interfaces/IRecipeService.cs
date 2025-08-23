using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Recipe;

namespace Fridges.Application.Interfaces;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe> GetAsync(Guid id);
    Task<Recipe> AddAsync(RecipeDto dto);
    Task DeleteAsync(Guid id);
    Task EditAsync(Guid id, RecipeDto dto);
    Task<List<RecipeProduct>> GetRecipeProductsAsync(Guid id);

}
