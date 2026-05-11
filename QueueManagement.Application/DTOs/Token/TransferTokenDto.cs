using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.Token;

public class TransferTokenDto
{
    public int TokenId { get; set; }

    [Range(1, int.MaxValue)]
    public int ToCategoryId { get; set; }

    public int? ToSubCategoryId { get; set; }

    [MaxLength(250)]
    public string? TransferReason { get; set; }
}