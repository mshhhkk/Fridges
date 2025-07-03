namespace Fridges.Domain.Enities;

class Fridges
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public short Capacity { get; set; }
    public bool IsFreezer { get; set; }
}
