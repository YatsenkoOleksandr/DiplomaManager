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
            return View();
        }

        public IActionResult GetTeachers()
        {
            var teachers = ProjectService.GetTeachers("ru");
            var teachersFio = teachers.Select(t =>
                new TeacherViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstNames[0].Name,
                    LastName = t.LastNames[0].Name,
                    Patronymic = t.Patronymics[0].Name,
                    PositionName = string.Empty
                });

            return Json(teachersFio);
        }

        public IActionResult GetDegrees()
        {
            var degrees = ProjectService.GetDegrees("ru");
            var degreesNames = degrees.Select(d =>
                new DegreeViewModel
                {
                    Id = d.Id,
                    Name = d.DegreeNames[0].Name
                });

            return Json(degreesNames);
        }

        public IActionResult GetDevelopmentAreas()
        {
            var das = ProjectService.GetDevelopmentAreas();

            return Json(das);
        }
    }
}
