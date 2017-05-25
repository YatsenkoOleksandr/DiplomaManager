using System.Collections.Generic;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class FormedProjectsViewModel
    {
        public IEnumerable<TeacherStudentsViewModel> ExistedUnchangedTeacherStudents { get; set; }

        public IEnumerable<TeacherStudentsViewModel> ExistedModifiedTeacherStudents { get; set; }

        public IEnumerable<TeacherStudentsViewModel> NewTeacherStudents { get; set; }

        public IEnumerable<TeacherStudentsViewModel> ConflictedTeacherStudents { get; set; }
    }
}
