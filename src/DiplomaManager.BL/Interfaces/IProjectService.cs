using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<TeacherDTO> GetTeachers();

        IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas();
    }
}
