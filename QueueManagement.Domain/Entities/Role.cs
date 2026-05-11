using QueueManagement.Domain.Common;

namespace QueueManagement.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}