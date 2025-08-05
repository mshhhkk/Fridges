using Fridges.Domain.Enums;

namespace Fridges.Domain.Enities.Products;

public class PlantProducts
{
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public bool IsOrganic { get; set; }
    public PlantProductsType Type { get; set; }
}
