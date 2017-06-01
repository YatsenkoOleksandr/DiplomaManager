using AutoMapper;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        #region Private methods

        private void CheckTeacherExistance(int teacherId)
        {
            if (_database.Teachers.Get(teacherId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного преподавателя.");
            }
        }

        private void CheckPredefensePeriodExistance(int predefensePeriodId)
        {
            if (_database.PredefensePeriods.Get(predefensePeriodId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного периода предзащит.");
            }
        }

        private void CheckPredefenseDateExistance(int predefenseDateId)
        {
            if (_database.PredefenseDates.Get(predefenseDateId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанный день проведения предзащиты.");
            }
        }

        private void CheckPredefenseExistance(int predefenseId)
        {
            if (_database.Predefenses.Get(predefenseId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного времени проведения предзащиты.");
            }
        }

        #endregion

        public IEnumerable<PredefensePeriodDTO> GetPredefensePeriods()
        {
            // Get info about predefense period - without predefense dates, with degrees
            IEnumerable<PredefensePeriod> periods = _database.PredefensePeriods.Get(
                new IncludeExpression<PredefensePeriod>(p => p.Degree.DegreeNames));
            return Mapper.Map<IEnumerable<PredefensePeriod>, IEnumerable<PredefensePeriodDTO>>(periods);
        }

        public IEnumerable<PredefenseDateDTO> GetPredefenseDates(int predefensePeriodId)
        {
            CheckPredefensePeriodExistance(predefensePeriodId);

            // List<PredefenseDateDTO>

            // Get predefense dates, sorts with
            FilterExpression<PredefenseDate>[] filters = new FilterExpression<PredefenseDate>[]
            {
                new FilterExpression<PredefenseDate>(pd => pd.PredefensePeriodId == predefensePeriodId)
            };
            IncludeExpression<PredefenseDate>[] includes = new IncludeExpression<PredefenseDate>[]
            {
                new IncludeExpression<PredefenseDate>(pd => pd.Predefenses),
                new IncludeExpression<PredefenseDate>(pd => pd.Appointments)
            };
            SortExpression<PredefenseDate, DateTime>[] sorts = new SortExpression<PredefenseDate, DateTime>[]
            {
                new SortExpression<PredefenseDate, DateTime>(pd => pd.Date, ListSortDirection.Ascending),
                new SortExpression<PredefenseDate, DateTime>(pd => pd.BeginTime, ListSortDirection.Ascending)
            };
            IEnumerable<PredefenseDate> predefenseDates = _database.PredefenseDates.Get(
                filters, includes, null, null, sorts);



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
