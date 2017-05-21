using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class AdminProjectsViewModel
    {
        public IEnumerable<ProjectShortInfo> Projects { get; set; }

        public ProjectFilterViewModel ProjectFilterViewModel { get; set; }

        public Pager Pager { get; set; }
    }
}
