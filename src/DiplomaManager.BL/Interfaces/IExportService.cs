using System.Collections.Generic;
using System.IO;
using DiplomaManager.BLL.Extensions.Admin;

namespace DiplomaManager.BLL.Services
{
    public interface IExportService
    {
        Stream GetTeacherStudentsStream(IEnumerable<TeacherStudents> teacherStudents);
    }
}