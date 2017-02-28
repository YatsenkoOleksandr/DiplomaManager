using System;
using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;

namespace DiplomaManager.BLL.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public int Id
        { get; set; }

        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }

        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }

        public int DevelopmentAreaId { get; set; }

        public DateTime CreationDate
        { get; set; }

        public DateTime? PracticeJournalPassed
        { get; set; }

        public bool? Accepted
        { get; set; }

        public List<ProjectTitleDTO> ProjectTitles { get; set; }
    }
}
