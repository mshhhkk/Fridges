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
    Task<List<Fridge>> GetAllFridges();
    Task DeleteFridge(Guid id);
    Task<Fridge> GetFridge(Guid id);
    Task<Fridge> AddFridge(FridgeDto dto);
    Task EditFridge(Guid id, FridgeDto dto);
}
