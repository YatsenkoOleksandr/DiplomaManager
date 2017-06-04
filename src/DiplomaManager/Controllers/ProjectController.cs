using System;
using System.Linq;
using DiplomaManager.Areas.Teacher.ViewModels;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class ProjectController : Controller
    {
        private IStudentProjectService RequestService { get; }

        public ProjectController(IStudentProjectService requestService)
        {
            RequestService = requestService;
        }

        public IActionResult GetTeachers(int? daId)
        {
            var teachers = RequestService.GetTeachers(daId, "ru");
            var teachersFio = teachers.Select(t =>
                new TeacherInfoViewModel
                {
                    Id = t.Id,
                    ShortName = t.GetShortName(193),
                    FullName = t.GetFullName(193),
                    PositionName = string.Empty
                });

            return Json(teachersFio);
        }

        public IActionResult GetDegrees()
        {
            var degrees = RequestService.GetDegrees("ru");
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
            var das = RequestService.GetDevelopmentAreas();

            return Json(das);
        }

        public IActionResult GetCapacity(int? degreeId, int? teacherId)
        {
            if (degreeId == null || teacherId == null) return NotFound();
            var capacity = RequestService.GetCapacity(degreeId.Value, teacherId.Value);
            return Json(capacity);
        }

        public IActionResult GetGraduationYears(int? degreeId)
        {
            if (degreeId == null) return NotFound();
            var years = RequestService.GetGraduationYears(degreeId.Value);
            return Json(years);
        }

        public IActionResult GetGroups(int? degreeId, int? graduationYear)
        {
            if (degreeId == null || graduationYear == null) return NotFound();
            var groups = RequestService.GetGroups(degreeId.Value, graduationYear.Value);
            return Json(groups);
        }

        public IActionResult GetStudents(int? groupId)
        {
            if (groupId == null) return NotFound();
            var students = RequestService.GetStudents(groupId.Value);
            var studentsFio = students.Select(s =>
                new UserInfoViewModel
                {
                    Id = s.Id,
                    ShortName = s.GetShortName(),
                    FullName = s.GetFullName(),
                });
            return Json(studentsFio);
        }

        [HttpPost]
        public IActionResult SendRequest([FromBody] RequestViewModel request)
        {
            var student = request.Student;
            var studentDto = new StudentDTO(student.Id, student.Email, student.GroupId);
            try
            {
                RequestService.CreateDiplomaRequest(studentDto, request.DaId, request.TeacherId, 1, request.Title);
                return Json(new { Message = "Заявка отравлена" });
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
