using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Recipe;
using Fridges.Persistance.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Persistance.Repositories;

public class RecipeRepository:IRecipeRepository
{
    private readonly AppDbContext _context;
    public RecipeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Recipe>> SearchRecipesByProductTypeId(int productTypeId)
    {
        var recipes = await _context.Recipes
        .Include(r => r.RecipeProducts)
        .Where(r => r.RecipeProducts.Any(rp => rp.ProductTypeId == productTypeId))
        .ToListAsync();
        return recipes;
    }
    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        return await _context.Recipes.ToListAsync();
    }
    public async Task<Recipe> GetRecipeByIdAsync(Guid id)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
    }
    public async Task AddRecipeAsync(Recipe recipe)
    {
        await _context.AddAsync(recipe);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteRecipeByIdAsync(Guid id)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        _context.Remove(recipe);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateRecipeInfoAsync(Recipe recipe)
    {
        await _context.SaveChangesAsync();
    }
    public async Task DeleteRecipeProductAsync(RecipeProduct product)
    {
        _context.RecipeProducts.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task CheckRecipeProductsAsync(Guid recipeId, List<RecipeProduct> products)
    {
        var currentProducts = await _context.RecipeProducts
         .Where(p => p.RecipeId == recipeId)
         .ToListAsync();
      
        foreach (var product in products)
        {
            var existing = currentProducts.FirstOrDefault(p => p.ProductTypeId == product.ProductTypeId);
            if (existing != null)
            {
                existing.Quantity = product.Quantity;
                existing.unitType = product.unitType;
            }
            else
            {
                _context.RecipeProducts.Add(new RecipeProduct
                {
                    RecipeId = recipeId,
                    ProductTypeId = product.ProductTypeId,
                    Quantity = product.Quantity,
                    unitType = product.unitType
                });
            }
        }

        foreach (var oldProduct in currentProducts)
        {
            if (!products.Any(p => p.ProductTypeId == oldProduct.ProductTypeId))
            {
                _context.RecipeProducts.Remove(oldProduct);
            }
        }
        await _context.SaveChangesAsync();
    }
    public async Task<List<RecipeProduct>> GetRecipeProductsById(Guid id)
    {
        return await _context.RecipeProducts.Where(r => r.RecipeId == id).ToListAsync();
    }
}
