using Fridges.Domain.Enums;

namespace Fridges.Domain.Enities.Products;

public class PlantProducts
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    public bool IsOrganic { get; set; }
    public PlantProductsType Type { get; set; }
}
