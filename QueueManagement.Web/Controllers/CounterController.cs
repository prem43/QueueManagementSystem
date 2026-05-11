using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueManagement.Application.DTOs.Counter;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Web.Controllers;

[Authorize(Roles = "Admin")]
public class CounterController : Controller
{
    private readonly ICounterService _counterService;

    public CounterController(
        ICounterService counterService)
    {
        _counterService = counterService;
    }

    public async Task<IActionResult> Index()
    {
        var counters =
            await _counterService.GetAllAsync();

        return View(counters);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCounterDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _counterService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var counter =
            await _counterService.GetByIdAsync(id);

        if (counter == null)
            return NotFound();

        var dto = new UpdateCounterDto
        {
            Id = counter.Id,
            Name = counter.Name,
            CounterNumber = counter.CounterNumber,
            IsActive = counter.IsActive
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        UpdateCounterDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _counterService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _counterService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}