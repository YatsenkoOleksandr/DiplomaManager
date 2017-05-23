using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
    public class DistributionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Distribute()
        {
            return View("Index");
        }
    }
}
