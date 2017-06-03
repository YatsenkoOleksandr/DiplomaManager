using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.Extensions.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Interfaces.PredefenseService
{
    public interface ITeacherPredefenseService
    {
        IEnumerable<TeacherPredefensePeriod> GetTeacherPredefensePeriods(int teacherId);

        IEnumerable<PredefenseSchedule> GetTeacherPredefenseSchedule(int teacherId, int predefensePeriodId);

        IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int predefensePeriodId);

        PredefenseDTO GetPredefenseResults(int predefenseId);

        void SavePredefenseResults(int teacherId, PredefenseDTO predefense);

        void SubmitToPredefenseDate(int teacherId, int predefenseDateId);

        void DenySubmitToPredefenseDate(int teacherId, int predefenseDateId);
    }
}
