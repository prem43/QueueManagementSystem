using QueueManagement.Domain.Enums;

namespace QueueManagement.Application.DTOs.Token;

public class TokenDto
{
    public int Id { get; set; }

    public string TokenNumber { get; set; } = string.Empty;

    public int QueueNumber { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? SubCategoryName { get; set; }

    public TokenStatus Status { get; set; }

    public string? CustomerName { get; set; }
    public string? CounterName { get; set; }
    public DateTime GeneratedAt { get; set; }
}