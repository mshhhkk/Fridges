using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities.Recipe;
using Fridges.Persistance.Interfaces;

namespace Fridges.Application.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();
        return recipes;
    }

    public async Task<Recipe> GetAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        return recipe;
    }

    public async Task<Recipe> AddAsync(RecipeDto dto)
    {
        var recipe = new Recipe
        {
            Id = Guid.NewGuid(),
            title = dto.title,
            Instructions = dto.Instructions,
            RecipeProducts = dto.Products.Select(p => new RecipeProduct
            {
                ProductTypeId = p.ProductTypeId,
                Quantity = p.Quantity,
                unitType = p.UnitType
            }).ToList()
        };
        await _recipeRepository.AddAsync(recipe);
        return recipe;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _recipeRepository.DeleteAsync(id);
    }

    public async Task EditAsync(Guid id, RecipeDto dto)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        recipe.title = dto.title;
        recipe.Instructions = dto.Instructions;
        List<RecipeProduct> products = dto.Products
             .Select(p => new RecipeProduct
             {
                 RecipeId = id,
                 ProductTypeId = p.ProductTypeId,
                 Quantity = p.Quantity,
                 unitType = p.UnitType
             })
             .ToList();

        await _recipeRepository.CheckRecipeProductsAsync(id, products);
        await _recipeRepository.UpdateAsync(recipe);
    }

    public async Task<List<RecipeProduct>> GetRecipeProductsByIdAsync(Guid id)
    {
        var products = await _recipeRepository.GetRecipeProductsByIdAsync(id);
        return products;
    }
}
