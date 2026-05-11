using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QueueManagement.Application.DTOs.SubCategory;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Web.Controllers;
[Authorize(Roles = "Admin")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryService _subCategoryService;

    private readonly ICategoryService _categoryService;

    public SubCategoryController(ISubCategoryService subCategoryService,ICategoryService categoryService)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var subCategories = await _subCategoryService.GetAllAsync();

        return View(subCategories);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(dto);
        }

        await _subCategoryService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var subCategory = await _subCategoryService.GetByIdAsync(id);

        if (subCategory == null)
            return NotFound();

        var categories = await _categoryService.GetAllAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        var dto = new UpdateSubCategoryDto
        {
            Id = subCategory.Id,
            Name = subCategory.Name,
            Prefix = subCategory.Prefix,
            CategoryId = subCategory.CategoryId,
            IsActive = subCategory.IsActive
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateSubCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(dto);
        }

        await _subCategoryService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        await _subCategoryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}