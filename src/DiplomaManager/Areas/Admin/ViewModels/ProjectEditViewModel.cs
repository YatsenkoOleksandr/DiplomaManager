using DiplomaManager.BLL.DTOs.ProjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class ProjectEditViewModel
    {
        public Dictionary<int, string> FreeStudents { get; set; }

        public Dictionary<int, string> FreeTeachers { get; set; }

        public ProjectDTO Project { get; set; }
    }
}
