using Fridges.Application.DTOs;
using Fridges.Domain.Enities;
namespace Fridges.Application.Interfaces;

public interface IFridgeService
{
    Task<List<Fridge>> GetAllAsync();
    Task DeleteAsync(Guid id);
    Task<Fridge> GetAsync(Guid id);
    Task<Fridge> AddAsync(FridgeDto dto);
    Task EditAsync(Guid id, FridgeDto dto);
}
