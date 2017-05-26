using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IAdminService
    {
        // Method for reciving projects/requests according to selected filters

        IEnumerable<ProjectDTO> GetProjects(PageInfo pageInfo, ProjectFilter filter);

        int CountProjects(ProjectFilter filter);


        // Methods for reciving filters

        IDictionary<int, string> GetStudents(ProjectFilter filter);

        IDictionary<int, string> GetTeachers(ProjectFilter filter);

        IDictionary<int, string> GetDegrees(ProjectFilter filter);

        IDictionary<int, string> GetYears(ProjectFilter filter);

        IDictionary<int, string> GetGroups(ProjectFilter filter);
    }
}
