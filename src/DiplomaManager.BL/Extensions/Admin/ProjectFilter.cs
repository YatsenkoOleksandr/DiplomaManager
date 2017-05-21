using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Extensions.Admin
{
    public class ProjectFilter
    {
        public ProjectFilter()
        {

        }

        public ProjectFilter(ProjectFilter filter)
        {
            TeacherId = filter.TeacherId;
            StudentId = filter.StudentId;
            DegreeId = filter.DegreeId;
            GraduationYear = filter.GraduationYear;
            GroupId = filter.GroupId;
            PracticeJournalStatus = filter.PracticeJournalStatus;
            AcceptanceStatus = filter.AcceptanceStatus;
            Title = filter.Title;
            SortedField = filter.SortedField;
            SortDirection = filter.SortDirection;
        }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public int DegreeId { get; set; }

        public int GraduationYear { get; set; }

        public PracticeJournalStatus PracticeJournalStatus { get; set; }

        public int GroupId { get; set; }

        public ProjectAcceptanceStatus AcceptanceStatus { get; set; }

        public string Title { get; set; }

        public ProjectSortedField SortedField { get; set; }

        public ListSortDirection SortDirection { get; set; }
    }
}
