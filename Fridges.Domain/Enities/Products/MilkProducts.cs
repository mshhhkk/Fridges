using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Products;

public class MilkProducts
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    public float FatPercent { get; set; }
    public MilkProductsType Type { get; set; }
}
