using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Controllers
{
    public class PredefenseController: Controller
    {
        private readonly IStudentPredefenseService _service;

        public PredefenseController(IStudentPredefenseService service)
        {
            _service = service;
        }

        public IActionResult GetDegrees()
        {
            try
            {
                IEnumerable<DegreeDTO> degrees = _service.GetDegrees();
                return Json(degrees);
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }

        public IActionResult GetGraduationYears(int degreeId)
        {
            try
            {
                IEnumerable<int> years = _service.GetGraduationYears(degreeId);
                return Json(years);
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }

        public IActionResult GetPredefenseSchedule(int degreeId, int graduationYear)
        {
            try
            {
                IEnumerable<PredefenseSchedule> schedule = _service.GetPredefenseSchedule(degreeId, graduationYear);
                return Json(schedule);
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }

        public IActionResult GetGroups(int degreeId, int graduationYear)
        {
            try
            {
                IEnumerable<GroupDTO> groups = _service.GetGroups(degreeId, graduationYear);
                return Json(groups);
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }

        public IActionResult GetFreeStudents(int groupId)
        {
            try
            {
                IEnumerable<StudentDTO> freeStudents = _service.GetFreeStudents(groupId);
                return Json(freeStudents);
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }

        [HttpPost]
        public IActionResult SubmitToPredefense([FromBody] int studentId, [FromBody]int predefenseId)
        {
            try
            {
                _service.SubmitPredefense(studentId, predefenseId);
                return Json("Ok");
            }
            catch (NoEntityInDatabaseException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (IncorrectActionException exc)
            {
                return Json(new { Error = exc.ToString(), ErrorMessage = exc.Message });
            }
            catch (Exception exc)
            {
                return Json(exc.InnerException == null
                    ? new { Error = exc.ToString(), ErrorMessage = exc.Message }
                    : new { Error = exc.InnerException.ToString(), ErrorMessage = exc.InnerException.Message });
            }
        }
    }
}
