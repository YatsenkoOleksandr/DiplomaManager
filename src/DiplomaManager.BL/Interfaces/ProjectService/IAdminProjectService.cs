using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces.ProjectService
{
    public interface IAdminProjectService
    {
        
        // Method for reciving projects/requests according to selected filters

        IEnumerable<ProjectDTO> GetProjects(PageInfo pageInfo, ProjectFilter filter);

        int CountProjects(ProjectFilter filter);

        /*
         
        // Methods for reciving filters

        IEnumerable<StudentDTO> GetStudents(ProjectFilter filter);

        IEnumerable<TeacherDTO> GetTeachers(ProjectFilter filter);

        IEnumerable<DegreeDTO> GetDegrees(ProjectFilter filter);

        IDictionary<int, string> GetYears(ProjectFilter filter);

        IEnumerable<GroupDTO> GetGroups(ProjectFilter filter);

        // Methods for editing 
        */


        // Methods for reciving free students and teachers

        IEnumerable<StudentDTO> GetFreeStudents(int degreeId, int graduationYear);

        IEnumerable<TeacherDTO> GetFreeTeachers(int degreeId, int graduationYear);

        // Method for accept requests
        void AcceptRequest(int projectId, int studentId, int teacherId, bool acceptance);

        void DeleteProject(int projectId);
    }
}
