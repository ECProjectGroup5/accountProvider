using Microsoft.AspNetCore.Mvc;

namespace accountProvider.Controllers
{
    public class SplashScreenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/start")]
        public IActionResult Start()
        {
            return View();
        }
    }
}
