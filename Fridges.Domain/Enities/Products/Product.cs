using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridges.Domain.Enities.Products;

abstract class Product
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public DateOnly Release { get; set; }

    public DateOnly Expiration { get; set; }
   
    public float Weight { get; set; }

    public bool IsFresh { get; set; }
}
