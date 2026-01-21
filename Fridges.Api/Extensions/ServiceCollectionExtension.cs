using Fridges.Application.Interfaces;
using Fridges.Application.Services;
using Fridges.Persistance.Interfaces;
using Fridges.Persistance.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fridges.Api.Extensions;

static public class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFridgeRepository, FridgeRepository>();
        services.AddScoped<IFridgeService, FridgeService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();


        return services;
    }
    

}
