using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Recipe;


namespace Fridges.Persistance.Interfaces;

public interface IRecipeRepository
{
    Task<List<Recipe>> SearchRecipesByProductTypeId(int productTypeId);
    Task<List<Recipe>> GetAllRecipesAsync();
    Task<Recipe> GetRecipeByIdAsync(Guid Id);
    Task AddRecipeAsync(Recipe recipe);
    Task DeleteRecipeByIdAsync(Guid id);
    Task UpdateRecipeInfoAsync(Recipe recipe);
    Task DeleteRecipeProductAsync(RecipeProduct product);
    Task CheckRecipeProductsAsync(Guid recipeId, List<RecipeProduct> products);
    Task<List<RecipeProduct>> GetRecipeProductsById(Guid id);
}
