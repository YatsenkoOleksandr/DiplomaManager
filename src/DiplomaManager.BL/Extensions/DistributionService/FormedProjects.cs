using DiplomaManager.BLL.DTOs.ProjectDTOs;
using System.Collections.Generic;

namespace DiplomaManager.BLL.Extensions.DistributionService
{
    /// <summary>
    /// Class contains formed pairs
    /// </summary>
    public class FormedProjects
    {
        public List<ProjectDTO> ExistedUnchangedProjects { get; set; }

        public List<ProjectDTO> ExistedModifiedProjects { get; set; }

        public List<ProjectDTO> NewProjects { get; set; }

        public List<ProjectDTO> ConflictedProjects { get; set; }
    }
}
