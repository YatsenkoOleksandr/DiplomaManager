using DiplomaManager.BLL.Extensions.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class ProjectFilterViewModel
    {
        public ProjectFilterViewModel()
        { }

        public ProjectFilterViewModel(int page, ProjectFilterViewModel filter)
        {
            this.Page = page;
            this.Size = filter.Size;
            A = filter.A;
            D = filter.D;
            Y = filter.Y;
            G = filter.G;
            J = filter.J;
            SD = filter.SD;
            SF = filter.SF;
            S = filter.S;
            T = filter.T;
            Ti = filter.Ti;
        }

        public ProjectFilterViewModel(
            ProjectSortedField sortedField, 
            ListSortDirection sortDirection, 
            ProjectFilterViewModel filter)
        {
            Page = filter.Page;
            Size = filter.Size;
            A = filter.A;
            D = filter.D;
            Y = filter.Y;
            G = filter.G;
            J = filter.J;
            S = filter.S;
            T = filter.T;
            Ti = filter.Ti;

            SD = sortDirection;
            SF = sortedField;
        }

        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public int T { get; set; }

        public int S { get; set; }

        public int D { get; set; }

        public int Y { get; set; }

        public PracticeJournalStatus J { get; set; }

        public int G { get; set; }

        public ProjectAcceptanceStatus A { get; set; }

        public string Ti { get; set; }

        public ProjectSortedField SF { get; set; }

        public ListSortDirection SD { get; set; }

        public ProjectFilter ToProjectFilter()
        {
            ProjectFilter filter = new ProjectFilter();
            filter.AcceptanceStatus = A;
            filter.DegreeId = D;
            filter.GraduationYear = Y;
            filter.GroupId = G;
            filter.PracticeJournalStatus = J;
            filter.SortDirection = SD;
            filter.SortedField = SF;
            filter.StudentId = S;
            filter.TeacherId = T;
            filter.Title = Ti;
            
            return filter;
        }
    }
}
