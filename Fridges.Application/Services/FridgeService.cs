using Fridges.Application.DTOs;
using Fridges.Application.Interfaces;
using Fridges.Domain.Enities;
using Fridges.Persistance.Interfaces;

namespace Fridges.Application.Services;

public class FridgeService : IFridgeService
{
    private readonly IFridgeRepository _fridgesRepo;

    public FridgeService(IFridgeRepository fridgeRepository)
    {
        _fridgesRepo = fridgeRepository;
    }

    public async Task<Result<List<Fridge>>> GetAllAsync()
    {
        var fridges = await _fridgesRepo.GetAllAsync();

        if (fridges == null)
        {
            return Result<List<Fridge>>.Failure("No fridges found");
        }

        return Result<List<Fridge>>.Success(fridges);

    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var fridge = await _fridgesRepo.GetByIdAsync(id);
        if (fridge == null)
        {
            return Result.Failure($"Fridge with id {id} does't exist");
        }

        await _fridgesRepo.DeleteAsync(id);

        return Result.Success();
    }

    public async Task<Result<Fridge>> GetAsync(Guid id)
    {
        var fridge = await _fridgesRepo.GetByIdAsync(id);
        if (fridge == null)
        {
            return Result<Fridge>.Failure($"Fridge with id {id} does't exist");
        }

        return Result<Fridge>.Success(fridge);
    }

    public async Task<Result<Fridge>> AddAsync(FridgeDto dto)
    {
        var fridge = new Fridge
        {
            Id = Guid.NewGuid(),
            Capacity = dto.Capacity,
            IsFreezer = dto.IsFreezer,
            Name = dto.Name
        };

        await _fridgesRepo.AddAsync(fridge);

        return Result<Fridge>.Success(fridge);
    }

    public async Task<Result<Fridge>> EditAsync(Guid id, FridgeDto dto)
    {
        var fridge = await _fridgesRepo.GetByIdAsync(id);
        if (fridge == null)
        {
            return Result<Fridge>.Failure($"Fridge with id {id} does't exist");
        }

        fridge.Id = id;
        fridge.Name = dto.Name;
        fridge.Capacity = dto.Capacity;

        await _fridgesRepo.UpdateAsync(fridge);

        return Result<Fridge>.Success(fridge);
    }
}
