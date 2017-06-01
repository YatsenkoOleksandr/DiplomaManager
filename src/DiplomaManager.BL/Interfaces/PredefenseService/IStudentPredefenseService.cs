using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.Extensions.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces.PredefenseService
{
    public interface IStudentPredefenseService
    {
        IEnumerable<DegreeDTO> GetDegrees();

        IEnumerable<int> GetGraduationYears(int degreeId);
        
        IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int degreeId, int graduationYear);

        IEnumerable<GroupDTO> GetGroups(int degreeId, int graduationYear);

        IEnumerable<StudentDTO> GetFreeStudents(int groupId);        

        void SubmitPredefense(int studentId, int predefenseId);
        
    }
}
