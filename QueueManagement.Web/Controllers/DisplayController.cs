using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QueueManagement.Web.Controllers;
[Authorize(Roles = "Admin,Staff")]
public class DisplayController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}