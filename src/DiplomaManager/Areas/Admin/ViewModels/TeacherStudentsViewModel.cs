using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using System.Collections.Generic;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class TeacherStudentsViewModel
    {
        public TeacherDTO Teacher { get; set; }

        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
