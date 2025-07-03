using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Products;

class MeatProducts:Product
{
    public bool IsFrozen { get; set; }
    public MeatProductsType Type { get; set; }
}


