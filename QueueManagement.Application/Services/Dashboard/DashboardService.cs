using QueueManagement.Application.DTOs.Dashboard;
using QueueManagement.Application.Interfaces;
using QueueManagement.Domain.Enums;

namespace QueueManagement.Application.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardDto> GetDashboardDataAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .FindAsync(t => !t.IsDeleted);

        var pendingTokens = tokens.Count(
            t => t.Status == TokenStatus.Waiting);

        var servingTokens = tokens.Count(
            t => t.Status == TokenStatus.Serving);

        var completedTokens = tokens.Count(
            t => t.Status == TokenStatus.Completed);

        var skippedTokens = tokens.Count(
            t => t.Status == TokenStatus.Skipped);

        var transferredTokens = tokens.Count(
            t => t.Status == TokenStatus.Transferred);

        double averageWaitTime = 0;

        var completedWithTimes = tokens
            .Where(t =>
                t.CalledAt.HasValue
                && t.GeneratedAt != default);

        if (completedWithTimes.Any())
        {
            averageWaitTime =
                completedWithTimes
                .Average(t =>
                    (t.CalledAt!.Value -
                     t.GeneratedAt)
                    .TotalMinutes);
        }

        var currentServing = tokens
            .Where(t => t.Status == TokenStatus.Serving)
            .Select(t => t.TokenNumber)
            .ToList();

        return new DashboardDto
        {
            PendingTokens = pendingTokens,

            ServingTokens = servingTokens,

            CompletedTokens = completedTokens,

            SkippedTokens = skippedTokens,

            TransferredTokens = transferredTokens,

            AverageWaitTimeMinutes =
                Math.Round(averageWaitTime, 2),

            CurrentServingTokens = currentServing
        };
    }
}