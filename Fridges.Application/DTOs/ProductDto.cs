using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enities;

namespace Fridges.Application.DTOs;

public class ProductDto
{
   
    public Guid FridgeId { get; set; }
    public DateOnly Release { get; set; }

    public DateOnly Expiration { get; set; }

    public float Weight { get; set; }

    public bool IsFresh { get; set; }

    public int ProductTypeId { get; set; }
}
