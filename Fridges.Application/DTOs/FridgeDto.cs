namespace Fridges.Application.DTOs;

public class FridgeDto
{
    public string Name { get; set; } = string.Empty;
    public short Capacity { get; set; }
    public bool IsFreezer { get; set; }
}