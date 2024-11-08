using accountProvider.ViewModels;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace accountProvider.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, DataContext context) : Controller
{
    private readonly DataContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public IActionResult SignUp()
    {
        return View();
    }

	[HttpPost]
	public async Task<IActionResult> SignUp(SignUpViewModel model)
	{
        if (ModelState.IsValid)
        {
          if (!await _context.Users.AnyAsync(x => x.Email == model.Email))
          {
                var userEntity = new UserEntity
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                if ((await _userManager.CreateAsync(userEntity, model.Password)).Succeeded)
                {
                    if ((await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false)).Succeeded)
                        return LocalRedirect("/");
                    else LocalRedirect("/signin");
				}
				else
				{
					ViewData["StatusMessage"] = "Something went wrong. Try again later";
				}
			}
          else
          { 
		    ViewData["StatusMessage"] = "User with the same email already exists";
		  }
          
        }

		return View(model);
	}

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

    /// <summary>
    /// Logs out and redirects the user to the frontpage.
    /// </summary>
    /// <returns></returns>
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Default");
    }
}