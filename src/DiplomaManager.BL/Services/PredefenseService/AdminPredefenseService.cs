using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using DiplomaManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    public class AdminPredefenseService: IAdminPredefenseService
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public AdminPredefenseService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }


        public IEnumerable<PredefensePeriodDTO> GetPredefensePeriods()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PredefenseDateDTO> GetPredefenseDates(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public PredefenseDTO GetPredefense(int predefenseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PredefenseTeacherCapacityDTO> GetPredefenseTeachers(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeacherDTO> GetFreeTeachersToPeriod(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeacherDTO> GetFreeTeachersToPredefense(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentDTO> GetFreeStudents(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public void SavePredefense(PredefenseDTO predefense)
        {
            throw new NotImplementedException();
        }


        public void CreatePredefensePeriod(PredefensePeriodDTO predefensePeriod)
        {
            throw new NotImplementedException();
        }

        public void DeletePredefensePeriod(int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public void EditPredefensePeriod(PredefensePeriodDTO predefensePeriod)
        {
            throw new NotImplementedException();
        }


        public void CreatePredefenseDate(PredefenseDateDTO predefenseDate)
        {
            throw new NotImplementedException();
        }

        public void DeletePredefenseDate(int predefenseDateId)
        {
            throw new NotImplementedException();
        }

        public void EditPredefenseDate(PredefensePeriodDTO predefensePeriod)
        {
            throw new NotImplementedException();
        }


        public void CreatePredefenseTeacher(PredefenseTeacherCapacityDTO teacherCapacity)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeacher(int teacherId, int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public void EditTeacherCapacity(PredefenseTeacherCapacityDTO capacity)
        {
            throw new NotImplementedException();
        }


        public void SubmitStudentToPredefense(int predefenseId, int studentId)
        {
            throw new NotImplementedException();
        }

        public void DenySubmitStudent(int predefenseId, int studentId)
        {
            throw new NotImplementedException();
        }

        public void SubmitTeacherToPredefenseDate(int predefenseDateId, int teacherId)
        {
            throw new NotImplementedException();
        }

        public void DenySubmitTeacher(int predefenseDateId, int teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
