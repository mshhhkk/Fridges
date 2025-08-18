using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Recipe;
using Fridges.Persistance.Interfaces;
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
}
