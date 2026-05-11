using QueueManagement.Domain.Common;

namespace QueueManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Prefix { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    public ICollection<Token> Tokens { get; set; } = new List<Token>();
}