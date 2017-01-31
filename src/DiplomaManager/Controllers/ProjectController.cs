using System.Linq;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class ProjectController : Controller
    {
        private IRequestService ProjectService { get; }

        public ProjectController(IRequestService projectService)
        {
            ProjectService = projectService;
        }

        public IActionResult Index()
        {
            var teachers = ProjectService.GetTeachers("ru");
            var teachersFio = teachers.Select(t =>
                new TeacherViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstNames[0].Name,
                    LastName = t.LastNames[0].Name,
                    Patronymic = t.Patronymics[0].Name
                });

            var degrees  = ProjectService.GetDegrees("ru");
            var degreesNames = degrees.Select(d =>
                new DegreeViewModel
                {
                    Id = d.Id,
                    Name = d.DegreeNames[0].Name
                });

            ViewBag.Degrees = degreesNames;
            ViewBag.Teachers = teachersFio;
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
