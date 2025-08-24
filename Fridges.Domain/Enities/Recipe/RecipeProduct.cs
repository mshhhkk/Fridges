using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Recipe;

public class RecipeProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RecipeId { get; set; }

    public Recipe Recipe { get; set; }

    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }

    public float Quantity { get; set; }
    public UnitType unitType { get; set; }
}
