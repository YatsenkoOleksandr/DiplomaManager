using System;
using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using System.Linq;
using DiplomaManager.DAL.Entities.StudentEntities;

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

        public string Comment
        { get; set; }

        public List<ProjectTitleDTO> ProjectTitles { get; set; }

        public string AcceptanceToString()
        {
            string res = string.Empty;
            switch(Accepted)
            {
                case null:
                    res = "Заявка активна";
                    break;

                case false:
                    res = "Заявка отклонена";
                    break;

                case true:
                    res = "Заявка принята";
                    break;                
            }
            return res;
        }

        public string DegreeToString(int localeId = 193)
        {
            string res;
            DegreeName degreeName = Student.Group.Degree.DegreeNames.FirstOrDefault(d => d.LocaleId == localeId);
            if (degreeName != null && !string.IsNullOrEmpty(degreeName.Name))
            {
                res = degreeName.Name;
            }
            else
            {
                res = "-";
            }
            return res;
        }

        public string PracticeJournalPassDateToString()
        {
            return (PracticeJournalPassed == null ? "-" : PracticeJournalPassed.ToString());
        }

        public string TitleToString(int localeId = 1)
        {
            string res;
            ProjectTitleDTO title = ProjectTitles.FirstOrDefault(t => t.LocaleId == localeId);
            if (title != null)
            {
                res = string.IsNullOrEmpty(title.Title) ? "-" : title.Title;
            }
            else
            {
                res = "-";
            }
            return res;
        }
    }
}
