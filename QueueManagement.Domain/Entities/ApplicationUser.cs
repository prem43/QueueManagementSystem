using Microsoft.AspNetCore.Identity;

namespace QueueManagement.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    public int? CounterId { get; set; }

    public Counter? Counter { get; set; }
}