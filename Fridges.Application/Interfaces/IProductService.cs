using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Application.DTOs;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
namespace Fridges.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetAsync (Guid id);
    Task<Product> AddAsync(ProductDto dto);
    Task EditAsync(Guid Id,EditProductDto dto);
    Task DeleteAsync(Guid id);
    Task<List<Recipe>> SearchRecipesByIdAsync(Guid id);
    Task<List<Product>> SearchProductsByCategoryAsync(ProductCategory productCategory);

}
