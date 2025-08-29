namespace Fridges.Application.DTOs;

public class EditProductDto
{
    public string FridgeId { get; set; }
    public float Weight { get; set; }
    public bool IsFresh { get; set; }
}
