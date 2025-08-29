namespace Fridges.Application.DTOs;

public class AddProductDto
{
    public string FridgeId { get; set; }
    public string Release { get; set; }
    public string Expiration { get; set; }
    public float Weight { get; set; }
    public bool IsFresh { get; set; }
    public int ProductTypeId { get; set; }
}
