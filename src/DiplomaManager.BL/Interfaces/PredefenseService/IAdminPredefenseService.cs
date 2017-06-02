using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Extensions.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces.PredefenseService
{
    public interface IAdminPredefenseService
    {
        IEnumerable<PredefensePeriodDTO> GetPredefensePeriods();

        IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int predefensePeriodId);

        PredefenseDTO GetPredefense(int predefenseId);

        IEnumerable<PredefenseTeacherCapacityDTO> GetPredefenseTeachers(int predefensePeriodId);

        IEnumerable<TeacherDTO> GetFreeTeachersToPeriod(int predefensePeriodId);

        IEnumerable<TeacherDTO> GetFreeTeachersToPredefenseDate(int predefenseDateId);

        IEnumerable<StudentDTO> GetFreeStudents(int predefensePeriodId);

        void SavePredefense(PredefenseDTO predefense);


        void CreatePredefensePeriod(PredefensePeriodDTO predefensePeriod);

        void DeletePredefensePeriod(int predefensePeriodId);

        void EditPredefensePeriod(PredefensePeriodDTO predefensePeriod);


        void CreatePredefenseDate(PredefenseDateDTO predefenseDate);

        void DeletePredefenseDate(int predefenseDateId);

        void EditPredefenseDate(PredefensePeriodDTO predefensePeriod);


        void CreatePredefenseTeacher(PredefenseTeacherCapacityDTO teacherCapacity);

        void DeleteTeacher(int teacherId, int predefensePeriodId);

        void EditTeacherCapacity(PredefenseTeacherCapacityDTO capacity);


        void SubmitStudentToPredefense(int predefenseId, int studentId);

        void DenySubmitStudent(int predefenseId, int studentId);

        void SubmitTeacherToPredefenseDate(int predefenseDateId, int teacherId);

        void DenySubmitTeacher(int predefenseDateId, int teacherId);
    }
}
