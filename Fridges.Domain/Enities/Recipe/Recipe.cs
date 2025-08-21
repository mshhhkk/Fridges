
namespace Fridges.Domain.Enities.Recipe;

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string title { get; set; }
    public string Instructions { get; set; }

    public List<RecipeProduct> RecipeProducts { get; set; } = new List<RecipeProduct>();
}
