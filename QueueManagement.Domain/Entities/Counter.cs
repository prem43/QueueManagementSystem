using QueueManagement.Domain.Common;

namespace QueueManagement.Domain.Entities;

public class Counter : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int CounterNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Token> Tokens { get; set; } = new List<Token>();
}