using System.Collections.Generic;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<TeacherViewModel> Teachers { get; set; }

        public IEnumerable<DevelopmentAreaViewModel> DevelopmentAreas { get; set; }

        public IEnumerable<string> Titles { get; set; }
    }
}
