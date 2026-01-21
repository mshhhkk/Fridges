using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities;
using Fridges.Application.DTOs;
namespace Fridges.Application.Interfaces;

public interface IFridgeService
{
    Task<List<Fridge>> GetAllAsync();
    Task DeleteAsync(Guid id);
    Task<Fridge> GetAsync(Guid id);
    Task<Fridge> AddAsync(FridgeDto dto);
    Task EditAsync(Guid id, FridgeDto dto);
}