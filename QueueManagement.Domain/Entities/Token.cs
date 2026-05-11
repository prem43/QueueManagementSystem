using QueueManagement.Domain.Common;
using QueueManagement.Domain.Enums;

namespace QueueManagement.Domain.Entities;

public class Token : BaseEntity
{
    public string TokenNumber { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public int? CounterId { get; set; }

    public string? AssignedUserId { get; set; }

    public ApplicationUser? AssignedUser { get; set; }
    public TokenStatus Status { get; set; } = TokenStatus.Created;

    public string? CustomerName { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CalledAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public string? Remarks { get; set; }

    public Category Category { get; set; } = null!;

    public SubCategory? SubCategory { get; set; }

    public Counter? Counter { get; set; }

    public int QueueNumber { get; set; }

    public DateTime? ServingStartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool IsSkipped { get; set; } = false;
}