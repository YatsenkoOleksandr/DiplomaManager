using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService ProjectService { get; }

        public ProjectController(IProjectService projectService)
        {
            ProjectService = projectService;
        }

        public IActionResult Index()
        {
            //ViewBag.Teachers = ProjectService.GetTeachers();
            ViewBag.DevelopmentAreas = ProjectService.GetDevelopmentAreas();

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProjectViewModel pvm)
        {
            return View();
        }
    }
}
