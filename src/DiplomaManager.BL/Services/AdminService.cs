using DiplomaManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;

namespace DiplomaManager.BLL.Services
{
    public class AdminService : IAdminService
    {
        public IEnumerable<ProjectDTO> GetProjects(PageInfo pageInfo, ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetDegrees(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetGroups(ProjectFilter filter)
        {
            throw new NotImplementedException();
        } 

        public IDictionary<int, string> GetStudents(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetTeachers(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetYears(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
