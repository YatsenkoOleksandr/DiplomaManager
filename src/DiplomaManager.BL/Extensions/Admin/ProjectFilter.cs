using System;
using System.Collections.Generic;
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

        public int Year { get; set; }

        public int GroupId { get; set; }

        public ProjectAcceptance Acceptance { get; set; }

        public string Title { get; set; }

        public ProjectSortedField SortedField { get; set; }
    }
}
