﻿using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IStudentProjectService
    {
        DevelopmentAreaDTO GetDevelopmentArea(int id);
        IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas();
        void AddDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void DeleteDevelopmentArea(int id);

        IEnumerable<DegreeDTO> GetDegrees(string cultureName = null);
        IEnumerable<TeacherDTO> GetTeachers(int? daId = null, string cultureName = null);
        CapacityDTO GetCapacity(int degreeId, int teacherId);
        IEnumerable<int> GetGraduationYears(int degreeId);
        IEnumerable<GroupDTO> GetGroups(int degreeId, int? graduationYear = null);
        IEnumerable<StudentDTO> GetStudents(int groupId);
        void CreateDiplomaRequest(StudentDTO studentDto, int daId, int teacherId, int localeId, string title);

        void Dispose();
    }
}