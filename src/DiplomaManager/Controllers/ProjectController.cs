using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
