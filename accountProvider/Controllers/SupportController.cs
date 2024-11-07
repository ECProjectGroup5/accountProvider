using Microsoft.AspNetCore.Mvc;

namespace accountProvider.Controllers
{
    public class SupportController : Controller
    {
        [Route("/supporttelephone")]
        public IActionResult SupportTelephone()
        {
            return View();
        }
    }
}
