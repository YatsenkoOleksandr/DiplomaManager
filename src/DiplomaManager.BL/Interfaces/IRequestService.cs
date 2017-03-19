using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IRequestService
    {
        DevelopmentAreaDTO GetDevelopmentArea(int id);
        IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas();
        void AddDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void DeleteDevelopmentArea(int id);

        IEnumerable<DegreeDTO> GetDegrees(string cultureName = null);
        IEnumerable<TeacherDTO> GetTeachers(int? daId = null, string cultureName = null);
        CapacityDTO GetCapacity(int degreeId, int teacherId);
        IEnumerable<GroupDTO> GetGroups(int degreeId);
        IEnumerable<PeopleNameDTO> GetStudentNames(string query, NameKindDTO nameKindDto, int maxItems = 10);
        void CreateDiplomaRequest(StudentDTO studentDto, int daId, int teacherId, int localeId, string title);

        void Dispose();
    }
}
