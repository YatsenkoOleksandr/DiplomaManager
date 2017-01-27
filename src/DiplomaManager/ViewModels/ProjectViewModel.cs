using System.Collections.Generic;

namespace DiplomaManager.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<TeacherViewModel> Teachers { get; set; }

        public IEnumerable<DevelopmentAreaViewModel> DevelopmentAreas { get; set; }
    }
}
