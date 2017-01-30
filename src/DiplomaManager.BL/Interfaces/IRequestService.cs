using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IRequestService
    {
        DevelopmentAreaDTO GetDevelopmentArea(int id);
        IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas();
        void AddDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void DeleteDevelopmentArea(int id);

        IEnumerable<TeacherDTO> GetTeachers(string cultureName = null);

        void Dispose();
    }
}
