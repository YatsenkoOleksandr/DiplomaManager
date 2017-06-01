using DiplomaManager.BLL.Interfaces.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Utils;
using AutoMapper;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    public class TeacherPredefenseService : ITeacherPredefenseService
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;        

        public TeacherPredefenseService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

        #region Private Methods
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

        private void CheckTeacherAccessToPredefensePeriod(int teacherId, int predefensePeriodId)
        {
            IEnumerable<PredefenseTeacherCapacity> capacities =
                _database.PredefenseTeacherCapacities.Get(
                    new FilterExpression<PredefenseTeacherCapacity>(ptc =>
                    ptc.TeacherId == teacherId && ptc.PredefensePeriodId == predefensePeriodId));
            if (capacities.Count() == 0)
            {
                throw new IncorrectActionException("Преподаватель не имеет доступа к периоду проведения предзащит.");
            }
        }
        

        private void CheckTeacherAccessToPredefense(int teacherId, int predefenseId)
        {
            Predefense pred = _database.Predefenses.Get(predefenseId);

            CheckPredefenseDateExistance(pred.PredefenseDateId);
            PredefenseDate predDate = _database.PredefenseDates.Get(pred.PredefenseDateId);

            Appointment appointment = _database.Appointments.Get(new FilterExpression<Appointment>(ap => 
                    ap.PredefenseDateId == predDate.Id && ap.TeacherId == teacherId))
                .FirstOrDefault();
            if (appointment == null)
            {
                throw new IncorrectActionException("Преподаватель не имеет доступа к редактированию результатов предзащиты.");
            }
        }

#endregion

        public IEnumerable<TeacherPredefensePeriod> GetTeacherPredefensePeriods(int teacherId)
        {
            // Check, if exists teacher
            CheckTeacherExistance(teacherId);

            // Get teacher capacities and periods, ordered by start and finish dates
            IEnumerable<PredefenseTeacherCapacity> teacherCapacities = _database.PredefenseTeacherCapacities.Get(
                new FilterExpression<PredefenseTeacherCapacity>[]
                {
                    new FilterExpression<PredefenseTeacherCapacity>(ptc => ptc.TeacherId == teacherId)
                },
                new IncludeExpression<PredefenseTeacherCapacity>[]
                {
                    new IncludeExpression<PredefenseTeacherCapacity>(ptc => ptc.PredefensePeriod)
                },
                null,
                null,
                new SortExpression<PredefenseTeacherCapacity, DateTime>[]
                {
                    new SortExpression<PredefenseTeacherCapacity, DateTime>(
                        ptc => ptc.PredefensePeriod.StartDate,
                        System.ComponentModel.ListSortDirection.Descending),
                    new SortExpression<PredefenseTeacherCapacity, DateTime>(
                        ptc => ptc.PredefensePeriod.FinishDate,
                        System.ComponentModel.ListSortDirection.Descending)
                });

            List<TeacherPredefensePeriod> teacherPeriods = new List<TeacherPredefensePeriod>();

            foreach(var ptc in teacherCapacities)
            {
                // Get from capacity period and add to teacher predefense period
                TeacherPredefensePeriod teacherPeriod = new TeacherPredefensePeriod();
                teacherPeriod.PredefenseCapacity = Mapper.Map<PredefenseTeacherCapacity, PredefenseTeacherCapacityDTO>(ptc);
                teacherPeriod.PredefensePeriod = Mapper.Map<PredefensePeriod, PredefensePeriodDTO>(ptc.PredefensePeriod);
                teacherPeriods.Add(teacherPeriod);
            }
            return teacherPeriods;
        }

        public IEnumerable<PredefenseSchedule> GetTeacherPredefenseSchedule(int teacherId, int predefensePeriodId)
        {            
            CheckTeacherExistance(teacherId);
            CheckPredefensePeriodExistance(predefensePeriodId);
            CheckTeacherAccessToPredefensePeriod(teacherId, predefensePeriodId);

            PredefenseScheduler scheduler = new PredefenseScheduler(_database, _cultureConfiguration, _emailService);

            return scheduler.GetTeacherPredefenseSchedule(teacherId, predefensePeriodId);            
        }

        public PredefenseDTO GetPredefenseResults(int teacherId, int predefenseId)
        {
            CheckTeacherExistance(teacherId);
            CheckPredefenseExistance(predefenseId);

            Predefense predefense = _database.Predefenses.Get(
                new FilterExpression<Predefense>(p => p.Id == predefenseId),
                new IncludeExpression<Predefense>[]
                {
                    new IncludeExpression<Predefense>(p => p.Student),
                    new IncludeExpression<Predefense>(p => p.Student.Group),
                    new IncludeExpression<Predefense>(p => p.Student.PeopleNames)
                }).FirstOrDefault();

            return Mapper.Map<Predefense, PredefenseDTO>(predefense);
        }

        public void SavePredefenseResults(int teacherId, PredefenseDTO predefense)
        {
            CheckTeacherExistance(teacherId);
            CheckPredefenseExistance(predefense.Id);
            CheckTeacherAccessToPredefense(teacherId, predefense.Id);

            Predefense databasePredefense = _database.Predefenses.Get(predefense.Id);

            // Editing and saving results
            databasePredefense.Passed = predefense.Passed;
            databasePredefense.ControlSigned = predefense.ControlSigned;
            databasePredefense.EconomySigned = predefense.EconomySigned;
            databasePredefense.PresentationReadiness = predefense.PresentationReadiness;
            databasePredefense.ReportExist = predefense.ReportExist;
            databasePredefense.SafetySigned = predefense.SafetySigned;
            databasePredefense.SoftwareReadiness = predefense.SoftwareReadiness;
            databasePredefense.WritingReadiness = predefense.WritingReadiness;

            _database.Save();
        }

        public void SubmitToPredefenseDate(int teacherId, int predefenseDateId)
        {
            CheckTeacherExistance(teacherId);
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.SubmitTeacherToPredefenseDate(teacherId, predefenseDateId);
        }

        public void DenySubmitToPredefenseDate(int teacherId, int predefenseDateId)
        {
            CheckTeacherExistance(teacherId);
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.DenyTeacherSubmitToPredefenseDate(teacherId, predefenseDateId);
        }
    }
}
