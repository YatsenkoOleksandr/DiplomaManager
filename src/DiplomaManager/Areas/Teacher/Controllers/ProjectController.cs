using System;
using System.Linq;
using DiplomaManager.Areas.Teacher.Requests;
using DiplomaManager.Areas.Teacher.ViewModels;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;
using DiplomaManager.Requests;
using DiplomaManager.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Policy = "Teachers&Admins")]
    public class ProjectController : Controller
    {
        private ITeacherProjectService TeacherService { get; }

        public ProjectController(ITeacherProjectService teacherService)
        {
            TeacherService = teacherService;
        }

        [HttpPost]
        public IActionResult GetDiplomaRequests([FromBody] GetProjectsRequest getProjectsRequest)
        {
            if (getProjectsRequest.TeacherId == null) return NotFound();
            var projects = TeacherService.GetDiplomaRequests(getProjectsRequest.TeacherId.Value, 
                getProjectsRequest.Query, getProjectsRequest.CurrentPage, getProjectsRequest.Limit);
            var projectVms = projects.Select(p => new RequestTeacherViewModel
            {
                Id = p.Id,
                Student = new StudentInfoViewModel
                {
                    Id = p.StudentId,
                    ShortName = p.Student.GetShortName(),
                    FullName = p.Student.GetFullName(),
                    Email = p.Student.Email
                },
                Accepted = p.Accepted,
                CreationDate = p.CreationDate,
                PracticeJournalPassed = p.PracticeJournalPassed,
                ProjectTitles = p.ProjectTitles
            });
            var projectsCount = TeacherService.GetDiplomaRequestsCount(getProjectsRequest.TeacherId.Value,
                getProjectsRequest.Query);
            var pagedResponse = new PagedResponse<RequestTeacherViewModel>(projectVms, projectsCount);
            return Json(pagedResponse);
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
}
