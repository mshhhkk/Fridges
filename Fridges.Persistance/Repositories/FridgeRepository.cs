
using Fridges.Domain.Enities;
using Fridges.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Persistance.Repositories;

public class FridgeRepository:IFridgeRepository
{
    private readonly AppDbContext _context;

    public FridgeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fridge> GetFridgeByIdAsync(Guid id)
    {
        var fridge = await _context.Fridges
            .FirstOrDefaultAsync(f => f.Id == id);
        return fridge;
    }
    public async Task<int?> GetCapacityByIdAsync(Guid id)
    {
        var capacity = await _context.Fridges
            .Where(f => f.Id == id)
            .Select(f => f.Capacity)
            .FirstOrDefaultAsync();
        return capacity;

    }
    public async Task<List<Fridge>> GetAllAsync()
    {
        return await _context.Fridges.ToListAsync();
    }
    public async Task UpdateAsync(Fridge fridge)
    {
         _context.Fridges.Update(fridge);
         await _context.SaveChangesAsync();
    }
    public async Task AddAsync(Fridge fridge)
    {
        await _context.Fridges.AddAsync(fridge);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var fridge = await _context.Fridges.FirstOrDefaultAsync(f => f.Id == id);
        _context.Fridges.Remove(fridge);
        await _context.SaveChangesAsync();

    }
    public async Task<string> GetNameByIdAsync(Guid id)
    {
        var name = await _context.Fridges
            .Where(f => f.Id == id)
            .Select(f => f.Name)
            .FirstOrDefaultAsync();
        return name;
    }
    public async Task<bool?> IsFreezerById(Guid id)
    {
        var isFreezer = await _context.Fridges
            .Where(f => f.Id == id)
            .Select(f => f.IsFreezer)
            .FirstOrDefaultAsync();
        
        return isFreezer;
    }
}
