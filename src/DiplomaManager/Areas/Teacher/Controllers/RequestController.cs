using System;
using System.Linq;
using DiplomaManager.Areas.Teacher.ViewModels;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;
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
            var projects = TeacherService.GetDiplomaRequests(teacherId.Value);
            var projectVms = projects.Select(p => new RequestTeacherViewModel
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
                PracticeJournalPassed = p.PracticeJournalPassed,
                ProjectTitles = p.ProjectTitles
            });
            return Json(projectVms);
        }

        [HttpPost]
        public IActionResult EditDiplomaProject([FromBody] ProjectEdit project)
        {
            try
            {
                var updatedProject = TeacherService.EditDiplomaProject(project);
                return Json(updatedProject);
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException == null
                    ? new { Error = ex.ToString(), ErrorMessage = ex.Message }
                    : new { Error = ex.InnerException.ToString(), ErrorMessage = ex.InnerException.Message });
            }
        }

        [HttpPost]
        public IActionResult RespondDiplomaRequest([FromBody] RespondProjectRequest respondProjectRequest)
        {
            try
            {
                if (respondProjectRequest.ProjectId == null) return NotFound();
                TeacherService.RespondDiplomaRequest(respondProjectRequest.ProjectId.Value, respondProjectRequest.Accepted,
                    respondProjectRequest.Comment);
                return Json(new { Message = "Заявка успешно обработана" });
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException == null
                    ? new { Error = ex.ToString(), ErrorMessage = ex.Message }
                    : new { Error = ex.InnerException.ToString(), ErrorMessage = ex.InnerException.Message });
            }
        }
    }

    public class RespondProjectRequest
    {
        public int? ProjectId { get; set; }
        public bool? Accepted { get; set; }
        public string Comment { get; set; }
    }

}
