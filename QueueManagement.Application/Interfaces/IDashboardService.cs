using QueueManagement.Application.DTOs.Dashboard;

namespace QueueManagement.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardDataAsync();
}