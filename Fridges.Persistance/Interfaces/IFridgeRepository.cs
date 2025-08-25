using Fridges.Domain.Enities;

namespace Fridges.Persistance.Interfaces;

public interface IFridgeRepository
{
    Task<Fridge> GetByIdAsync(Guid FridgeId);
    Task<int?> GetCapacityByIdAsync(Guid id);
    Task<List<Fridge>> GetAllAsync();
    Task UpdateAsync(Fridge fridge);
    Task AddAsync(Fridge fridge);
    Task DeleteAsync(Guid id);
    Task<string> GetNameByIdAsync(Guid id);
    Task<bool?> IsFreezerByIdAsync(Guid id);
    Task<int> GetCurrentProductsAmountByIdAsync(Guid id);
}
