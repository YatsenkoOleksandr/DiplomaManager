using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.BLL.Interfaces.ProjectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
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

        public IActionResult DeleteProject(ProjectFilterViewModel filter, int projectId)
        {
            AdminProjectService.DeleteProject(projectId);

            return RedirectToAction("Index", filter);
        }

        public IActionResult EditProject(int projectId, int degreeId, int graduationYear)
        {
            ProjectDTO project = AdminProjectService.GetProject(projectId);

            Dictionary<int, string> freeStudents = new Dictionary<int, string>();
            freeStudents.Add(project.StudentId, project.Student.GetFullName());
            foreach(StudentDTO st in AdminProjectService.GetFreeStudents(degreeId, graduationYear))
            {
                if (st.Id != project.StudentId)
                {
                    freeStudents.Add(st.Id, st.GetFullName());
                }
            }

            Dictionary<int, string> freeTeachers = new Dictionary<int, string>();
            freeTeachers.Add(project.TeacherId, project.Teacher.GetFullName());
            foreach(TeacherDTO t in AdminProjectService.GetFreeTeachers(degreeId, graduationYear))
            {
                if (t.Id != project.TeacherId)
                {
                    freeTeachers.Add(t.Id, t.GetFullName());
                }
            }

            ProjectEditViewModel viewModel = new ProjectEditViewModel()
            {
                Project = project,
                FreeStudents = freeStudents,
                FreeTeachers = freeTeachers
            };


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditProject(int projectId, int studentId, int teacherId, bool acceptance)
        {
            AdminProjectService.AcceptRequest(projectId, studentId, teacherId, acceptance);
            return RedirectToAction("Index");
        }
    }
}
