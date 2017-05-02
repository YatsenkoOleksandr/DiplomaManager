using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestController: Controller
    {
        private IAdminService AdminService { get; }

        public RequestController(IAdminService adminService)
        {
            AdminService = adminService;
        }

        public IActionResult Index()
        {
            PageInfo pageInfo = new PageInfo()
            {
                Page = 1,
                PageSize = 10,
                TotalPages = 10
            };

            ProjectFilter projectFilter = new ProjectFilter()
            {
                SortDirection = System.ComponentModel.ListSortDirection.Ascending,
                SortedField = ProjectSortedField.PracticeJournalPassedDate
            };

            AdminService.GetProjects(pageInfo, projectFilter);

            return View();
        }
    }
}
