using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enums;
using Fridges.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Persistance.Repositories;

public class ProductRepository:IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetAllAsync()
    {
       return await _context.Products.ToListAsync();
    }
    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }
    public async Task AddAsync(Product product)
    {
        await _context.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        _context.Remove(product);
        await _context.SaveChangesAsync();
    }
    public async Task<int> GetProductTypeIdAsync(Guid id)
    {
        var productType= await _context.Products
            .Where(p => p.Id == id)
            .Select(p => p.ProductType)
            .FirstOrDefaultAsync();
        return productType.Id;
    }
    public async Task<List<Product>> GetAllByCategoryAsync(ProductCategory productCategory)
    {
        var products = await _context.Products
            .Include(p => p.ProductType)
            .Where(p => p.ProductType.category == productCategory)
            .ToListAsync();
        return products;
    }
}
