using Fridges.Domain.Enities.Products;
namespace Fridges.Domain.Enities;

public class Fridge
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public short Capacity { get; set; }
    public bool IsFreezer { get; set; }
    public List<Product> Products { get; set; }
}
