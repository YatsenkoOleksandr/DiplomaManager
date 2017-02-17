using System.Linq;
using DiplomaManager.Areas.Teacher.ViewModels;
using DiplomaManager.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class RequestController : Controller
    {
        private ITeacherService TeacherService { get; }

        public RequestController(ITeacherService teacherService)
        {
            TeacherService = teacherService;
        }

        public IActionResult GetDiplomaRequests(int? teacherId)
        {
            if (teacherId == null) return NotFound();
            var projects = TeacherService.GetDiplomaRequests(teacherId.Value, "ru");
            var projectVms = projects.Select(p =>
                new RequestTeacherViewModel
                {
                    Id = p.Id,
                    Student = new StudentViewModel
                    {
                        Id = p.StudentId,
                        FirstName = p.Student.FirstNames[0].Name,
                        LastName = p.Student.LastNames[0].Name,
                        Patronymic = p.Student.Patronymics[0].Name,
                        GroupName = p.Student.Group.Name,
                        Email = p.Student.Email
                    },
                    Accepted = p.Accepted,
                    CreationDate = p.CreationDate,
                    ProjectTitle = p.ProjectTitles[0].Title
                });
            return Json(projectVms);
        }
    }
}
