using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Interfaces
{
    public interface ITeacherProjectService
    {
        IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId, string query = "", int currentPage = 1,
            int itemsPerPage = 10);
        int GetDiplomaRequestsCount(int teacherId, string query = "");
        ProjectEdit EditDiplomaProject(ProjectEdit project);
        void RespondDiplomaRequest(int projectId, bool? accepted, string comment = null);
    }
}