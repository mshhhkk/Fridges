using System.ComponentModel.DataAnnotations;
using Fridges.Domain.Enities.Products;
namespace Fridges.Domain.Enities.Recipe;

class RecipeProducts
{
    [Required]
    public Guid RecipeId { get; set; }
    [Required]
    public Recipe Recipe { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public double AmountWeight { get; set; }
}
