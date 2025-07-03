using Fridges.Domain.Enums;

namespace Fridges.Domain.Enities.Products;

class PlantProducts: Product
{
    public bool IsRipe { get; set; }
    public PlantProductsTypes Type { get; set; }
}
