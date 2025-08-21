using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Recipe;

namespace Fridges.Application.Interfaces;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipeInfo(Guid id);
    Task<Recipe> AddRecipe(RecipeDto dto);
    Task DeleteRecipe(Guid id);
    Task EditRecipe(Guid id, RecipeDto dto);
    Task<List<RecipeProduct>> GetRecipeProducts(Guid id);

}
