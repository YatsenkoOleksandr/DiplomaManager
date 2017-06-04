using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
    public class AdminPredefenseController: Controller
    {
        private readonly IAdminPredefenseService _service;

        public AdminPredefenseController(IAdminPredefenseService service)
        {
            _service = service;
        }


#region Methods for predefense period
        public IActionResult Periods()
        {
            IEnumerable<PredefensePeriodDTO> periods = _service.GetPredefensePeriods();

            return View(periods);
        }

        public IActionResult GetDegrees()
        {
            IEnumerable<DegreeDTO> degreeDTOs = _service.GetDegrees();
            List<JsonObject> degrees = new List<JsonObject>();
            foreach (var degree in degreeDTOs)
            {
                degrees.Add(new JsonObject()
                {
                    Id = degree.Id,
                    Info = degree.GetName()
                });
            }
            return Json(degrees);
        }

        public IActionResult GetGraduationYears(int id)
        {
            IEnumerable<int> years = _service.GetGraduationYears(id);

            return Json(years);
        }

        public IActionResult CreatePeriod()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePeriod(PredefensePeriodViewModel period)
        {
            if (ModelState.IsValid)
            {
                PredefensePeriodDTO periodDTO = new PredefensePeriodDTO()
                {
                    DegreeId = period.DegreeId,
                    GraduationYear = period.GraduationYear,

                    StartDate = period.StartTime,
                    FinishDate = period.FinishTime,
                    PredefenseStudentTime = new TimeSpan(0, period.StudentTime, 0)
                };
                try
                {
                    _service.CreatePredefensePeriod(periodDTO);
                    return RedirectToAction("Periods");
                }
                catch (IncorrectParameterException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(period);
                }
                catch (Exception exc)
                {
                    return RedirectToAction("Periods");
                }
            }
            return View(period);
        }

        public IActionResult DeletePeriod(int predefensePeriodId)
        {
            try
            {
                _service.DeletePredefensePeriod(predefensePeriodId);
            }
            catch
            {

            }
            return RedirectToAction("Periods");
        }
#endregion

#region Methods for getting schedule and changing students, teachers, results
        public IActionResult Schedule(int predefensePeriodId)
        {
            try
            {
                IEnumerable<PredefenseSchedule> schedule = _service.GetPredefenseSchedule(predefensePeriodId);
                return View(new PredefensePeriodSchedule()
                {
                    PredefensePeriodId = predefensePeriodId,
                    PredefenseSchedule = schedule
                });
            }
            catch(Exception exc)
            {
                return RedirectToAction("Periods");
            }
        }

        public IActionResult PredefenseResults(int predefenseDateId, int predefensePeriodId)
        {
            try
            {
                PredefenseDTO predefense = _service.GetPredefense(predefenseDateId);
                PredefenseResultsViewModel results = new PredefenseResultsViewModel()
                {
                    Id = predefense.Id,
                    PredefensePeriodId = predefensePeriodId,
                    ControlSigned = predefense.ControlSigned,
                    EconomySigned = predefense.EconomySigned,
                    Passed = predefense.Passed,
                    PresentationReadiness = predefense.PresentationReadiness,
                    ReportExist = predefense.ReportExist,
                    SafetySigned = predefense.SafetySigned,
                    SoftwareReadiness = predefense.SoftwareReadiness,
                    WritingReadiness = predefense.WritingReadiness
                };
                return View(results);
            }
            catch
            {
                return RedirectToAction("Periods");
            }
        }

        

        public IActionResult ChangeStudent(int oldStudentId, int studentId, int predefenseId, int predefensePeriodId)
        {
            try
            {
                if (oldStudentId != 0)
                {
                    _service.DenySubmitStudent(predefenseId, oldStudentId);
                }
                if (studentId != 0)
                {
                    _service.SubmitStudentToPredefense(predefenseId, studentId);
                }
                return RedirectToAction("Schedule", new { predefensePeriodId = predefensePeriodId });
            }
            catch (Exception exc)
            {
                return RedirectToAction("Periods");
            }
        }

        public IActionResult ChangeTeacher(int oldTeacherId, int teacherId, int predefenseDateId, int predefensePeriodId)
        {
            try
            {
                if (oldTeacherId != 0)
                {
                    _service.DenySubmitTeacher(predefenseDateId, oldTeacherId);
                }
                if (teacherId != 0)
                {
                    _service.SubmitTeacherToPredefenseDate(predefenseDateId, teacherId);
                }
                return RedirectToAction("Schedule", new { predefensePeriodId = predefensePeriodId });
            }
            catch (Exception exc)
            {
                return RedirectToAction("Periods");
            }
        }

        [HttpPost]
        public IActionResult PredefenseResults(PredefenseResultsViewModel results)
        {
            if (ModelState.IsValid)
            {
                PredefenseDTO predefense = new PredefenseDTO()
                {
                    Id = results.Id,
                    ControlSigned = results.ControlSigned,
                    Passed = results.Passed,
                    EconomySigned = results.EconomySigned,
                    PresentationReadiness = results.PresentationReadiness,
                    ReportExist = results.ReportExist,
                    SafetySigned = results.SafetySigned,
                    SoftwareReadiness = results.SoftwareReadiness,
                    WritingReadiness = results.WritingReadiness
                };
                try
                {
                    _service.SavePredefense(predefense);
                    return RedirectToAction("Schedule", new { predefensePeriodId = results.PredefensePeriodId });
                }
                catch (NoEntityInDatabaseException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(results);
                }
                catch (IncorrectParameterException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(results);
                }
                catch (IncorrectActionException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(results);
                }
                catch (Exception exc)
                {
                    return RedirectToAction("Periods");
                }
            }
            return View(results);
        }
#endregion

#region Methods for creating teachers in period
        public IActionResult PeriodTeachers(int predefensePeriodId)
        {
            try
            {
                IEnumerable<PredefenseTeacherCapacityDTO> teachers = 
                    _service.GetPredefensePeriodTeachers(predefensePeriodId);
                return View(new PredefensePeriodTeachers()
                {
                    PeriodTeachers = teachers,
                    PredefensePeriodId = predefensePeriodId
                });
            }
            catch (Exception exc)
            {
                return RedirectToAction("Periods");
            }
        }

        public IActionResult AddPeriodTeacher(int predefensePeriodId)
        {
            PredefenseTeacherCapacityViewModel vm = new PredefenseTeacherCapacityViewModel()
            {
                PredefensePeriodId = predefensePeriodId
            };

            return View(vm);
        }

        public IActionResult GetFreePeriodTeachers(int id)
        {
            try
            {
                IEnumerable<TeacherDTO> freeTeachers = _service.GetFreeTeachersToPeriod(id);
                List<JsonObject> obj = new List<JsonObject>();

                foreach(var t in freeTeachers)
                {
                    obj.Add(new JsonObject()
                    {
                        Id = t.Id,
                        Info = t.GetFullName(194)
                    });
                }
                return Json(obj);
            }
            catch
            {
                return RedirectToAction("Periods");
            }
        }

        [HttpPost]
        public IActionResult AddPeriodTeacher(PredefenseTeacherCapacityViewModel teacherCapacity)
        {
            if (ModelState.IsValid)
            {
                PredefenseTeacherCapacityDTO ptc = new PredefenseTeacherCapacityDTO()
                {
                    Count = 0,
                    Total = teacherCapacity.Total,
                    PredefensePeriodId = teacherCapacity.PredefensePeriodId,
                    TeacherId = teacherCapacity.TeacherId
                };
                try
                {
                    _service.CreatePredefenseTeacher(ptc);
                    return RedirectToAction("PeriodTeachers", new { predefensePeriodId = teacherCapacity.PredefensePeriodId });
                }
                catch (NoEntityInDatabaseException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(teacherCapacity);
                }
                catch (IncorrectParameterException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(teacherCapacity);
                }
                catch (IncorrectActionException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(teacherCapacity);
                }
                catch (Exception exc)
                {
                    return RedirectToAction("Periods");
                }
            }
            return View(teacherCapacity);
        }

        public IActionResult DeletePeriodTeacher(int teacherId, int predefensePeriodId)
        {
            try
            {
                _service.DeleteTeacher(teacherId, predefensePeriodId);
                return RedirectToAction("PeriodTeachers", new { predefensePeriodId = predefensePeriodId });
            }
            catch (NoEntityInDatabaseException exc)
            {
                return RedirectToAction("Periods");
            }
            catch (IncorrectParameterException exc)
            {
                return RedirectToAction("Periods");
            }
            catch (IncorrectActionException exc)
            {
                return RedirectToAction("Periods");
            }
            catch (Exception exc)
            {
                return RedirectToAction("Periods");
            }
        }
#endregion

#region Methods for creating predefense date
        public IActionResult CreatePredefenseDate(int predefensePeriodId)
        {
            PredefenseDateViewModel date = new PredefenseDateViewModel()
            {
                PredefensePeriodId = predefensePeriodId
            };
            return View(date);
        }

        [HttpPost]
        public IActionResult CreatePredefenseDate(PredefenseDateViewModel predefenseDate)
        {
            if (ModelState.IsValid)
            {
                PredefenseDateDTO predfenseDate = new PredefenseDateDTO()
                {
                    BeginTime = predefenseDate.StartTime,
                    FinishTime = predefenseDate.FinishTime,
                    Date = predefenseDate.Date,
                    PredefensePeriodId = predefenseDate.PredefensePeriodId
                };
                try
                {
                    _service.CreatePredefenseDate(predfenseDate);
                    return RedirectToAction("Schedule", new { predefensePeriodId = predfenseDate.PredefensePeriodId });
                }
                catch (IncorrectParameterException exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(predefenseDate);
                }
            }
            return RedirectToAction("Periods");
        }

        public IActionResult DeletePredefenseDate(int predefenseDateId, int predefenseperiodId)
        {
            try
            {
                _service.DeletePredefenseDate(predefenseDateId);
                return RedirectToAction("Schedule", new { predefensePeriodId = predefenseperiodId });
            }
            catch
            {
                return RedirectToAction("Periods");
            }
        }
#endregion
    }
}
