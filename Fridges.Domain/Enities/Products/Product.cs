using Fridges.Domain.Enities.Recipe;

namespace Fridges.Domain.Enities.Products;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FridgeId { get; set; }

    public Fridge Fridge { get; set; }
   
    public DateOnly Release { get; set; }
    public DateOnly Expiration { get; set; }

    public float Weight { get; set; }
    public bool IsFresh { get; set; }

    public MeatProducts? meatProducts { get; set; }
    public MilkProducts? milkProducts { get; set; }
    public PlantProducts? plantProducts { get; set; }

    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }
}
