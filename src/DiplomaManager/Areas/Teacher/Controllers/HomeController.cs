using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
