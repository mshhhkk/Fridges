using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enums;

namespace Fridges.Persistance.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    Task<int> GetProductTypeIdAsync(Guid id);
    Task<List<Product>> GetAllByCategoryAsync(ProductCategory productCategory);
}
