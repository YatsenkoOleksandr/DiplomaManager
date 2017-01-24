using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
