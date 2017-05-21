using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.ProjectService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProjectController: Controller
    {
        private IAdminProjectService AdminProjectService { get; }

        public AdminProjectController(IAdminProjectService adminService)
        {
            AdminProjectService = adminService;
        }

        public IActionResult Index(ProjectFilterViewModel filter)
        {
            PageInfo pageInfo = new PageInfo()
            {
                Page = filter.Page,
                PageSize = filter.Size
            };
            ProjectFilter projectFilter = filter.ToProjectFilter();

            int total = this.AdminProjectService.CountProjects(projectFilter); 
            IEnumerable<ProjectDTO> projectDtos =  this.AdminProjectService
                .GetProjects(pageInfo, projectFilter);

            IEnumerable<ProjectShortInfo> projects =
                AutoMapper.Mapper.Map<IEnumerable<ProjectDTO>, IEnumerable<ProjectShortInfo>>(projectDtos);

            Pager pager = new ViewModels.Pager(
                total, 
                filter.Page, 
                filter.Size);

            AdminProjectsViewModel viewModel = new AdminProjectsViewModel()
            {
                Pager = pager,
                ProjectFilterViewModel = filter,
                Projects = projects                
            };

            return View(viewModel);
        }
    }
}
