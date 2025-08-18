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
    Task<List<Product>> GetProductsList();
    Task<Product> GetProductInfo(Guid id);
    Task<Product> AddProduct(ProductDto dto);
    Task EditProductInfo(Guid Id,EditProductDto dto);
    Task DeleteProduct(Guid id);
    Task<List<Recipe>> SearchRecipesByProduct(Guid id);
    Task<List<Product>> SearchProductsByCategory(ProductCategory productCategory);

}
