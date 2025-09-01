using FluentValidation;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Application.Services;
using Fridges.Application.Validators.Fridges;
using Fridges.Application.Validators.Products;
using Fridges.Application.Validators.Recipes;
using Fridges.Persistance.Interfaces;
using Fridges.Persistance.Repositories;
using Fridges.Api.Exeptions;

namespace Fridges.Api.Extensions;

static public class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<FridgeDto>, AddFridgeValidator>();
        services.AddScoped<IValidator<AddProductDto>, AddProductValidator>();
        services.AddScoped<IValidator<EditProductDto>, EditProductValidator>();
        services.AddScoped<IValidator<RecipeDto>, AddRecipeValidator>();
        services.AddScoped<IValidator<RecipeProductDto>, RecipeProductValidator>();
        services.AddScoped<IFridgeRepository, FridgeRepository>();
        services.AddScoped<IFridgeService, FridgeService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IRecipeService,RecipeService>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddExceptionHandler<GlobalExeptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}
