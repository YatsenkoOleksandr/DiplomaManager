﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
﻿using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
    public class DistributionController : Controller
    {

        private IDistributionService DistributionService { get; }

        public DistributionController(IDistributionService distributionService)
        {
            DistributionService = distributionService;
        }

        public IActionResult Index()
        {
            var projectGroups = GetAcceptedProjects(1, 0);
            return View(projectGroups);
        }

        public IActionResult Distribute()
        {
            return View("Index");
        }

        private IEnumerable<TeacherStudentsViewModel> GetAcceptedProjects(int degreeId, int graduationYear)
        {
            var acceptedProjects = DistributionService.GetAcceptedProjects(degreeId, graduationYear);
            var projectGroups = acceptedProjects.GroupBy(p => p.Teacher)
                                                .Select(g => new TeacherStudentsViewModel
                                                {
                                                    Teacher = g.Key,
                                                    Students = g.Select(p => p.Student)
                                                });
            return projectGroups;
        }
    }
}
