using System.Threading.Tasks;
using Fridges.Application.DTOs;
using Fridges.Domain.Enities;
namespace Fridges.Application.Interfaces;

public interface IFridgeService
{
    Task<Result<List<Fridge>>> GetAllAsync();
    Task<Result> DeleteAsync(Guid id);
    Task<Result<Fridge>> GetAsync(Guid id);
    Task<Result<Fridge>> AddAsync(FridgeDto dto);
    Task<Result<Fridge>> EditAsync(Guid id, FridgeDto dto);
}
