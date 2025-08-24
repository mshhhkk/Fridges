using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Products;

public class MeatProducts
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    public bool IsFrozen { get; set; }
    public MeatProductsType Type { get; set; }
   
}


