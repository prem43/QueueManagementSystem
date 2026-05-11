using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueueManagement.Application.DTOs.Auth;
using QueueManagement.Domain.Entities;

namespace QueueManagement.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController( SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var result = await _signInManager
            .PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                false,
                false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(
                "",
                "Invalid username or password");

            return View(dto);
        }

        return RedirectToAction(
            "Index",
            "Token");
    }
    
  
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}