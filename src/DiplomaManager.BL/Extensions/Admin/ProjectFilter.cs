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
