using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueManagement.Application.DTOs.Category;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Web.Controllers;
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var dto = new UpdateCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Prefix = category.Prefix,
            Description = category.Description,
            IsActive = category.IsActive
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _categoryService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _categoryService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}