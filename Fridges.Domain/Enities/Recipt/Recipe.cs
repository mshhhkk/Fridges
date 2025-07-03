
namespace Fridges.Domain.Enities.Recipe;

class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public string Instructions { get; set; }

    public ICollection<RecipeProducts> RecipeProducts { get; set; }


}
