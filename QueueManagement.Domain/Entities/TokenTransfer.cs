using QueueManagement.Domain.Common;

namespace QueueManagement.Domain.Entities;

public class TokenTransfer : BaseEntity
{
    public int TokenId { get; set; }

    public int FromCategoryId { get; set; }

    public int? FromSubCategoryId { get; set; }

    public int ToCategoryId { get; set; }

    public int? ToSubCategoryId { get; set; }

    public string? TransferReason { get; set; }

    public string? TransferedByUserId { get; set; }

    public DateTime TransferedAt { get; set; }
        = DateTime.UtcNow;

    public Token Token { get; set; } = null!;

    public Category FromCategory { get; set; } = null!;

    public SubCategory? FromSubCategory { get; set; }

    public Category ToCategory { get; set; } = null!;

    public SubCategory? ToSubCategory { get; set; }
}