using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Products;

namespace Fridges.Application.DTOs;

public class FridgeDto
{
    public string Name { get; set; } = string.Empty;
    public short Capacity { get; set; }
    public bool IsFreezer { get; set; }
}
