using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enums;
namespace Fridges.Domain.Enities.Recipe;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProductCategory category { get; set; }
    public List<Product> Products { get; set; }
    public ICollection<RecipeProduct> RecipeProducts { get; set; } = new List<RecipeProduct>();
}
