using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;

namespace Fridges.Persistance.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
    Task<int> GetProductTypeIdAsync(Guid id);
    Task<List<Product>> SearchProductsByCategoryAsync(ProductCategory productCategory);
}
