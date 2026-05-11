using QueueManagement.Domain.Common;

namespace QueueManagement.Domain.Entities;

public class SubCategory : BaseEntity
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Prefix { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public Category Category { get; set; } = null!;

    public ICollection<Token> Tokens { get; set; } = new List<Token>();
}