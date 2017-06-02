using System.Collections.Generic;

namespace DiplomaManager.BLL.Extensions.Admin
{
    public class TeacherStudents
    {
        public string Teacher { get; set; }

        public IEnumerable<string> Students { get; set; }
    }
}
