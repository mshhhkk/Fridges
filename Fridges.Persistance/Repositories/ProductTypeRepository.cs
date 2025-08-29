using Fridges.Domain.Enities.Recipe;
using Fridges.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Fridges.Persistance.Repositories;

public class ProductTypeRepository:IProductTypeRepository
{
    private readonly AppDbContext _context;

    public ProductTypeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<int>> GetAllAsync()
    {
        var productTypes = await _context.ProductTypes.Select(pt => pt.Id).ToListAsync();

        return productTypes;
    }
}
