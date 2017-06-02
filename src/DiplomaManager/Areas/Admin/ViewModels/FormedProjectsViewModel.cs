using System.Collections.Generic;
using DiplomaManager.BLL.Extensions.Admin;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class FormedProjectsViewModel
    {
        public IEnumerable<TeacherStudents> ExistedUnchangedTeacherStudents { get; set; }

        public IEnumerable<TeacherStudents> ExistedModifiedTeacherStudents { get; set; }

        public IEnumerable<TeacherStudents> NewTeacherStudents { get; set; }

        public IEnumerable<TeacherStudents> ConflictedTeacherStudents { get; set; }
    }
}
