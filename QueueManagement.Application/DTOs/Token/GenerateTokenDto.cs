using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.Token;

public class GenerateTokenDto
{
    [Required]
    public int CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    [MaxLength(100)]
    public string? CustomerName { get; set; }

    [MaxLength(20)]
    public string? MobileNumber { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }
}