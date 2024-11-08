using Microsoft.AspNetCore.Mvc;

namespace accountProvider.Controllers
{
    public class Onboarding : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/onboarding")]
        public IActionResult OnBoarding()
        {
            return View();
        }
    }
}
