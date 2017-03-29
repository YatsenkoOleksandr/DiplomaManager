using System;
using System.Collections.Generic;
using System.Linq;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class RequestController : Controller
    {
        private IRequestService RequestService { get; }

        public RequestController(IRequestService requestService)
        {
            RequestService = requestService;
        }

        public IActionResult GetTeachers(int? daId)
        {
            var teachers = RequestService.GetTeachers(daId, "ru");
            var teachersFio = teachers.Select(t =>
                new TeacherViewModel
                {
                    Id = t.Id,
                    FirstName = t.GetFirstName(193),
                    LastName = t.GetLastName(193),
                    Patronymic = t.GetPatronymic(193),
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

        public IActionResult GetGroups(int? degreeId)
        {
            if (degreeId == null) return NotFound();
            var groups = RequestService.GetGroups(degreeId.Value);
            return Json(groups);
        }

        public IActionResult GetStudentNames(string query, NameKindDTO nameKind)
        {
            var names = RequestService.GetStudentNames(query, nameKind);
            return Json(names);
        }

        [HttpPost]
        public IActionResult SendRequest([FromBody] RequestViewModel request)
        {
            var student = request.Student;
            var studentDto = new StudentDTO(
                student.FirstName, student.LastName, student.Patronymic, 193, student.Email, DateTime.Now);
            try
            {
                RequestService.CreateDiplomaRequest(studentDto, request.DaId, request.TeacherId, 193, request.Title);
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
