using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId);
        void EditDiplomaProject(ProjectEdit project);
    }
}
