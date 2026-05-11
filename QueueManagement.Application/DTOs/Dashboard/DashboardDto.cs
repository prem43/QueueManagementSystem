namespace QueueManagement.Application.DTOs.Dashboard;

public class DashboardDto
{
    public int PendingTokens { get; set; }

    public int ServingTokens { get; set; }

    public int CompletedTokens { get; set; }

    public int SkippedTokens { get; set; }

    public int TransferredTokens { get; set; }

    public double AverageWaitTimeMinutes { get; set; }

    public IEnumerable<string> CurrentServingTokens{ get; set; } = new List<string>();
}