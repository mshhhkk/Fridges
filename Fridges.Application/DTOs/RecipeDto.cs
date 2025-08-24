using Fridges.Domain.Enums;

namespace Fridges.Application.DTOs;

public class RecipeDto
{
    public string title { get; set; }
    public string Instructions { get; set; }
    public List<RecipeProductDto> Products { get; set; } = new();
}

public class RecipeProductDto
{
    public int ProductTypeId { get; set; }
    public float Quantity { get; set; }
    public UnitType UnitType { get; set; }
}
