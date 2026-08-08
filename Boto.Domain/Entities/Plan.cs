namespace Boto.Domain.Entities;

public class Plan
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxAgents { get; set; }
    public int MaxMessagesPerMonth { get; set; }
}