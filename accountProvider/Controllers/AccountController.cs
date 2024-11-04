using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace accountProvider.Controllers;


[Authorize]
public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
