using accountProvider.ViewModels;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace accountProvider.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, DataContext context) : Controller
{

    private readonly DataContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;




    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }

    //Inloggning av användare
    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        //försök logga in om giltig
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }

            }
        }

        ViewData["StatusMessage"] = "Incorrect credentials, try again.";
        return View(viewModel);
    }

}
