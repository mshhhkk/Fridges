using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Recipe;


namespace Fridges.Persistance.Interfaces;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllByProductTypeIdAsync(int productTypeId);
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe> GetByIdAsync(Guid Id);
    Task AddAsync(Recipe recipe);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Recipe recipe);
    Task DeleteRecipeProductAsync(RecipeProduct product);
    Task CheckRecipeProductsAsync(Guid recipeId, List<RecipeProduct> products);
    Task<List<RecipeProduct>> GetRecipeProductsByIdAsync(Guid id);
}
