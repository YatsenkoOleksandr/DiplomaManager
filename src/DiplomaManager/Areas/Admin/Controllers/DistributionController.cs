using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.BLL.Extensions.DistributionService;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
    public class DistributionController : Controller
    {
        private IDistributionService DistributionService { get; }
        private IExportService ExportService { get; }

        public DistributionController(IDistributionService distributionService, IExportService exportService)
        {
            DistributionService = distributionService;
            ExportService = exportService;
        }

        public IActionResult Index()
        {
            var acceptedProjects = DistributionService.GetAcceptedProjects(1, DateTime.Now.Year);
            var projectGroups = ConvertToTeacherStudents(acceptedProjects);
            return View(projectGroups);
        }

        public IActionResult Distribute()
        {
            var formedProjects = DistributionService.Distribute(1, DateTime.Now.Year);
            var formedProjectsVms = new FormedProjectsViewModel
            {
                ConflictedTeacherStudents = ConvertToTeacherStudents(formedProjects.ConflictedProjects),
                ExistedModifiedTeacherStudents = ConvertToTeacherStudents(formedProjects.ExistedModifiedProjects
                                                                                        .Where(p => p.Accepted == true)),
                ExistedUnchangedTeacherStudents = ConvertToTeacherStudents(formedProjects.ExistedUnchangedProjects),
                NewTeacherStudents = ConvertToTeacherStudents(formedProjects.NewProjects)
            };
            //TempData["FormedProjects"] = formedProjects;
            return View(formedProjectsVms);
        }

        public IActionResult Save()
        {
            if (TempData["FormedProjects"] is FormedProjects formedProjects)
            {
                //DistributionService.Save(1, DateTime.Now.Year, formedProjects);
                ViewBag.Message = "Обработанные заявки успешно сохранены";
            }
            else
            {
                ViewBag.Message = "Ошибка обработки заявок";
            }
            return View("Distribute");
        }

        public IActionResult Export()
        {
            var acceptedProjects = DistributionService.GetAcceptedProjects(1, DateTime.Now.Year);
            if (acceptedProjects == null) return NotFound();
            var teacherStudents = ConvertToTeacherStudents(acceptedProjects);
            if (teacherStudents != null)
            {
                var excelStream = ExportService.GetTeacherStudentsStream(teacherStudents);
                const string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                const string fileName = "teacher_students.xlsx";
                return File(excelStream, fileType, fileName);
            }
            return NotFound();
        }

        private static IEnumerable<TeacherStudents> ConvertToTeacherStudents(IEnumerable<ProjectDTO> projects)
        {
            return projects.GroupBy(p => p.Teacher)
                                                .Select(g => new TeacherStudents
                                                {
                                                    Teacher = g.Key.GetFullName(193),
                                                    Students = g.Select(p => p.Student.GetFullName()).Distinct()
                                                });
        }
    }
}
