using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DiplomaManager.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Policy = "Teachers&Admins")]
    public class TeacherPredefenseController: Controller
    {
        private readonly ITeacherPredefenseService _service;

        public TeacherPredefenseController(ITeacherPredefenseService service)
        {
            _service = service;
        }

        public IActionResult GetTeacherPeriods(int teacherId)
        {
            try
            {
                IEnumerable<TeacherPredefensePeriod> periods = _service.GetTeacherPredefensePeriods(teacherId);
                return Json(periods);
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

        public IActionResult GetTeacherPredefenseSchedule(int teacherId, int predefensePeriodId)
        {
            try
            {
                IEnumerable<PredefenseSchedule> teacherSchedule = _service.GetTeacherPredefenseSchedule(
                    teacherId, 
                    predefensePeriodId);
                return Json(teacherSchedule);
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

        public IActionResult GetPredefenseSchedule(int predefensePeriodId)
        {
            try
            {
                IEnumerable<PredefenseSchedule> schedule = _service.GetPredefenseSchedule(                    
                    predefensePeriodId);
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

        public IActionResult GetPredefenseResults(int predefenseId)
        {
            try
            {
                PredefenseDTO predefenseResults = _service.GetPredefenseResults(predefenseId);
                return Json(predefenseResults);
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
        public IActionResult SavePredefenseResults([FromBody]int teacherId, [FromBody]PredefenseDTO predefense)
        {
            try
            {
                _service.SavePredefenseResults(teacherId, predefense);
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

        [HttpPost]
        public IActionResult SubmitToPredefenseDate([FromBody]int teacherId, [FromBody]int predefenseDateId)
        {
            try
            {
                _service.SubmitToPredefenseDate(teacherId, predefenseDateId);
                return Json(new { Message = "Ok" });
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

        [HttpPost]
        public IActionResult DenySubmitToPredefenseDate([FromBody]int teacherId, [FromBody]int predefenseDateId)
        {
            try
            {
                _service.DenySubmitToPredefenseDate(teacherId, predefenseDateId);
                return Json(new { Message = "Ok" });
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
