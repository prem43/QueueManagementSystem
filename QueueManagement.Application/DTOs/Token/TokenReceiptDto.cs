namespace QueueManagement.Application.DTOs.Token;

public class TokenReceiptDto
{
    public string TokenNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; }
}