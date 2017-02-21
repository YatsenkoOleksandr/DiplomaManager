using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.ProjectDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId);
    }
}
