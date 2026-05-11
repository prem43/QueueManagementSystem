using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using QueueManagement.Application.DTOs.Token;
using QueueManagement.Application.Interfaces.Services;
using QueueManagement.Web.Hubs;
using System.Security.Claims;

namespace QueueManagement.Web.Controllers;

[Authorize(Roles = "Admin,Staff")]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    private readonly ICategoryService _categoryService;

    private readonly ISubCategoryService _subCategoryService;

    private readonly IHubContext<QueueHub> _hubContext;

    public TokenController(ITokenService tokenService, ICategoryService categoryService, ISubCategoryService subCategoryService,IHubContext<QueueHub> hubContext)
    {
        _tokenService = tokenService;

        _categoryService = categoryService;

        _subCategoryService = subCategoryService;

        _hubContext = hubContext;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.PendingTokens =
            await _tokenService
                .GetPendingTokensAsync();

        ViewBag.ServingTokens =
            await _tokenService
                .GetServingTokensAsync();

        ViewBag.CompletedTokens =
            await _tokenService
                .GetCompletedTokensAsync();

        ViewBag.SkippedTokens =
            await _tokenService
                .GetSkippedTokensAsync();

       

        return View();
    }

    public async Task<IActionResult> Create()
    {
        var categories =
            await _categoryService.GetAllAsync();

        ViewBag.Categories =
            new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenerateTokenDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories =
                await _categoryService.GetAllAsync();

            ViewBag.Categories =
                new SelectList(categories, "Id", "Name");

            return View(dto);
        }

        var tokenId =
            await _tokenService
                .GenerateTokenAsync(dto);

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Receipt(int id)
    {
        var receipt =
            await _tokenService
                .GetReceiptAsync(id);

        return View(receipt);
    }
    [HttpGet]
    public async Task<JsonResult> GetSubCategories(int categoryId)
    {
        var subCategories =
            await _subCategoryService
                .GetByCategoryIdAsync(categoryId);

        return Json(subCategories);
    }

    public async Task<IActionResult> CallNext()
    {
        try
        {
            var userId = User.FindFirstValue(
    ClaimTypes.NameIdentifier);

            var token =
                await _tokenService
                    .CallNextTokenAsync(userId!);

            if (token != null)
            {
                    await _hubContext.Clients.All
    .SendAsync(
        "ReceiveTokenCall",
        token.TokenNumber,
        token.CustomerName,
        token.CategoryName,
        token.CounterName);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Complete(int id)
    {
        await _tokenService
            .CompleteTokenAsync(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Skip(int id)
    {
        await _tokenService
            .SkipTokenAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Transfer(int id)
    {
        var categories =
            await _categoryService.GetAllAsync();

        ViewBag.Categories =
            new SelectList(categories, "Id", "Name");

        var dto = new TransferTokenDto
        {
            TokenId = id
        };

        return View(dto);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Recall(int id)
    {
        await _tokenService
            .RecallTokenAsync(id);

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Transfer(TransferTokenDto dto)
    {
        if (!ModelState.IsValid)
        {
            var categories =
                await _categoryService.GetAllAsync();

            ViewBag.Categories =
                new SelectList(categories, "Id", "Name");

            return View(dto);
        }

        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        await _tokenService.TransferTokenAsync(
            dto,
            userId!);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>ReOpen(int id)
    {
        await _tokenService
            .ReOpenTokenAsync(id);

        return RedirectToAction(nameof(Index));
    }
}