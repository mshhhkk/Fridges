using System.Collections.Generic;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities.Products;
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

    public async Task<Result<List<Recipe>>> GetAllAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();
        if (recipes == null)
        {
            return Result<List<Recipe>>.Failure("No recipes found");
        }
        else
        {
            return Result<List<Recipe>>.Success(recipes);
        }
    }

    public async Task<Result<Recipe>> GetAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if( recipe == null)
        {
            return Result<Recipe>.Failure($"Fridge with {id} not found");
        }
        return Result<Recipe>.Success(recipe);
    }

    public async Task<Result<Recipe>> AddAsync(RecipeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Instructions))
        {
             return Result<Recipe>.Failure("Instructions field should be filled");
        }
        if (string.IsNullOrWhiteSpace(dto.title))
        {
            return Result<Recipe>.Failure("Title field is required");
        }
        if (dto.Products == null)
        {
            return Result<Recipe>.Failure("Recipe should have products");
        }

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

        return Result<Recipe>.Success(recipe);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if(recipe == null)
        {
            return Result.Failure($"Recipe with {id} doesn't exist");
        }
        await _recipeRepository.DeleteAsync(id);
        return Result.Success();
    }

    public async Task<Result<Recipe>> EditAsync(Guid id, RecipeDto dto)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
        {
            return Result<Recipe>.Failure($"Recipe with {id} doesn't exist");
        }
        if (string.IsNullOrWhiteSpace(dto.Instructions))
        {
            return Result<Recipe>.Failure("Instructions field should be filled");
        }
        if (string.IsNullOrWhiteSpace(dto.title))
        {
            return Result<Recipe>.Failure("Title field is required");
        }
        if (dto.Products == null)
        {
            return Result<Recipe>.Failure("Recipe should have products");
        }

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
        return Result<Recipe>.Success(recipe);
    }

    public async Task<Result<List<RecipeProduct>>> GetRecipeProductsByIdAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
        {
            return Result<List<RecipeProduct>>.Failure($"Recipe with {id} doesn't exist");
        }
        var products = await _recipeRepository.GetRecipeProductsByIdAsync(id);
        return Result<List<RecipeProduct>>.Success(products);
    }
}
