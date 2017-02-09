using System;

namespace DiplomaManager.BLL.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public int Id
        { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        public int DevelopmentAreaId { get; set; }

        public DateTime CreationDate
        { get; set; }

        public DateTime? PracticeJournalPassed
        { get; set; }

        public bool Accepted
        { get; set; }
    }
}
