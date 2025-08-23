using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<List<Fridge>> GetAllAsync()
    {
        var fridges = await _fridgesRepo.GetAllAsync();
        return fridges;
    }
    public async Task DeleteAsync(Guid id)
    {
        var fridge = await _fridgesRepo.GetFridgeByIdAsync(id);
        if (fridge == null)
        {
            throw new InvalidOperationException("Fridge doesn't exist!");
        }
        await _fridgesRepo.DeleteAsync(id);

    }
    public async Task<Fridge> GetAsync(Guid id)
    {
        var fridge = await _fridgesRepo.GetFridgeByIdAsync(id);
        if (fridge == null)
        {
            throw new InvalidOperationException("Fridge doesn't exist!");
        }
        return fridge;
    }
    public async Task<Fridge> AddAsync(FridgeDto dto)
    {
        var fridge = new Fridge
        {
            Id = Guid.NewGuid(),
            Capacity = dto.Capacity,
            IsFreezer = dto.IsFreezer,
            Name = dto.Name
        };

        await _fridgesRepo.AddAsync(fridge);
        return fridge;
    }
    public async Task EditAsync(Guid id, FridgeDto dto)
    {
        var fridge = await _fridgesRepo.GetFridgeByIdAsync(id);
        fridge.Id = id;
        fridge.Name = dto.Name;
        fridge.Capacity = dto.Capacity;

        if (fridge == null)
        {
            throw new InvalidOperationException($"Fridge with id: {id} doesn't exist!");
        }

        await _fridgesRepo.UpdateAsync(fridge);
    }

}