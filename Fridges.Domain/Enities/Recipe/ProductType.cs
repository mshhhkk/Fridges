using Fridges.Domain.Enums;
using Fridges.Domain.Enities.Products;
namespace Fridges.Domain.Enities.Recipe;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProductCategory category { get; set; }
    public List<Product> Products { get; set; }
    public ICollection<RecipeProduct> RecipeProducts { get; set; } = new List<RecipeProduct>();

}
