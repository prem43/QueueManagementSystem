using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QueueManagement.Application.DTOs.Token;
using QueueManagement.Application.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace QueueManagement.Web.Controllers;

public class KioskController : Controller
{
    private readonly ICategoryService
        _categoryService;

    private readonly ISubCategoryService
        _subCategoryService;

    private readonly ITokenService
        _tokenService;
    private readonly IEmailService
    _emailService;

    private readonly ISmsService
        _smsService;
    public KioskController(
        ICategoryService categoryService,
        ISubCategoryService subCategoryService,
        ITokenService tokenService,
        IEmailService emailService,
        ISmsService smsService)
    {
        _categoryService =
            categoryService;

        _subCategoryService =
            subCategoryService;

        _tokenService =
            tokenService;
        _emailService = emailService;
        _smsService = smsService;
    }

    // Kiosk Screen

    public async Task<IActionResult> Index()
    {
        var categories =
            await _categoryService.GetAllAsync();

        ViewBag.Categories =
            new SelectList(
                categories,
                "Id",
                "Name");

        return View();
    }

    // Generate Token

    [HttpPost]
    public async Task<IActionResult> Generate(
     GenerateTokenDto dto,
     bool printReceipt,
     bool sendEmail,
     bool sendSms)
    {
        if (!ModelState.IsValid)
        {
            var categories =
                await _categoryService.GetAllAsync();

            ViewBag.Categories =
                new SelectList(
                    categories,
                    "Id",
                    "Name");

            return View(
                "Index",
                dto);
        }

        // Generate Token

        var tokenId =
            await _tokenService
                .GenerateTokenAsync(dto);

        // EMAIL

        try
        {
            if (sendEmail
                && !string.IsNullOrWhiteSpace(
                    dto.Email))
            {
                await _emailService
                    .SendEmailAsync(
                        dto.Email,
                        "Queue Token Generated",
                        $@"
                    <h2>
                        Queue Token
                    </h2>

                    <p>
                        Your token has been generated successfully.
                    </p>

                    <p>
                        Category:
                        <strong>
                            {dto.CategoryId}
                        </strong>
                    </p>

                    <p>
                        Thank you.
                    </p>");
            }
        }
        catch
        {
            TempData["EmailError"] =
                "Email sending failed.";
        }

        // SMS

        try
        {
            if (sendSms
                && !string.IsNullOrWhiteSpace(
                    dto.MobileNumber))
            {
                await _smsService
                    .SendSmsAsync(
                        dto.MobileNumber,
                        "Your queue token generated successfully.");
            }
        }
        catch
        {
            TempData["SmsError"] =
                "SMS sending failed.";
        }

        // PRINT

        if (printReceipt)
        {
            return RedirectToAction(
                "Receipt",
                "Token",
                new { id = tokenId });
        }

        TempData["Success"] =
            "Token generated successfully.";

        return RedirectToAction(
            nameof(Index));
    }

    // Load SubCategories

    [HttpGet]
    public async Task<JsonResult>
        GetSubCategories(int categoryId)
    {
        var subCategories =
            await _subCategoryService
                .GetByCategoryIdAsync(
                    categoryId);

        return Json(subCategories);
    }
}