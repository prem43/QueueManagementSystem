using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueManagement.Application.Interfaces;

namespace QueueManagement.Web.Controllers;

[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboard =
            await _dashboardService
                .GetDashboardDataAsync();

        return View(dashboard);
    }
}