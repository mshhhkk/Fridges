using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Products;

class MilkProducts:Product
{
    public int FatPercent { get; set; }
    public MilkProductsType Type { get; set; }
}
