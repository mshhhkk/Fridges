namespace Fridges.Application.DTOs;

public class EditProductDto
{
    public Guid FridgeId { get; set; }
    public DateOnly Release { get; set; }
    public DateOnly Expiration { get; set; }
    public float Weight { get; set; }
    public bool IsFresh { get; set; }
}
