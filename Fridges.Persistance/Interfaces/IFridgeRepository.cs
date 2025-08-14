using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities;

namespace Fridges.Persistance.Interfaces;

public interface IFridgeRepository
{
    Task<Fridge> GetFridgeByIdAsync(Guid FridgeId);
    Task<int?> GetCapacityByIdAsync(Guid id);
    Task<List<Fridge>> GetAllAsync();
    Task UpdateAsync(Fridge fridge);
    Task AddAsync(Fridge fridge);
    Task DeleteAsync(Guid id);
    Task<string> GetNameByIdAsync(Guid id);
    Task<bool?> IsFreezerById(Guid id);

}
