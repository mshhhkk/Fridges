using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
using Fridges.Persistance.Interfaces;
using Fridges.Persistance.Repositories;

namespace Fridges.Application.Services;

public class RecipeService:IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    public async Task<List<Recipe>> GetAllRecipes()
    {
        var recipes = await _recipeRepository.GetAllRecipesAsync();
        return recipes;
    }
    public async Task<Recipe> GetRecipeInfo(Guid id)
    {
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
        return recipe;
    }
    public async Task<Recipe> AddRecipe(RecipeDto dto)
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

        await _recipeRepository.AddRecipeAsync(recipe);
        return recipe;
    }
    public async Task DeleteRecipe(Guid id)
    {
        await _recipeRepository.DeleteRecipeByIdAsync(id);
    }
    public async Task EditRecipe(Guid id, RecipeDto dto)
    {
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
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
        await _recipeRepository.UpdateRecipeInfoAsync(recipe);
        
        
    }

    public async Task<List<RecipeProduct>> GetRecipeProducts(Guid id)
    {
        var products = await _recipeRepository.GetRecipeProductsById(id);
        return products;
    }


    
}
