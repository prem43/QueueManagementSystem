using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QueueManagement.Application.DTOs.User;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Web.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ICounterService _counterService;
    public UserController(IUserService userService,ICounterService counterService)
    {
        _userService = userService;

        _counterService = counterService;
    }

    public async Task<IActionResult> Index()
    {
        var users =
            await _userService.GetAllAsync();

        return View(users);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Roles = new List<SelectListItem>
    {
        new("Admin", "Admin"),
        new("Staff", "Staff")
    };

        var counters =
            await _counterService.GetAllAsync();

        ViewBag.Counters =
            new SelectList(counters, "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles =
                new List<SelectListItem>
            {
            new("Admin", "Admin"),
            new("Staff", "Staff")
            };

            var counters =
                await _counterService.GetAllAsync();

            ViewBag.Counters =
                new SelectList(
                    counters,
                    "Id",
                    "Name");

            return View(dto);
        }

        await _userService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
}