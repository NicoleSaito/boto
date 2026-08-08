namespace Boto.Domain.Entities;

public class Agent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty; // Feature: RAG 
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!; // Propriedade de navegação: Ela não vira uma coluna no banco — Forma de acessar agent.User.Name diretamente, sem precisar fazer um JOIN manual
}