using AutoMapper;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    class PredefenseScheduler
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public PredefenseScheduler(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

        public IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int degreeId, int graduationYear)
        {
            // Predefense schedule (result)
            List<PredefenseSchedule> predefenseSchedules = new List<PredefenseSchedule>();

            // Get periods which have same degreeId and graduation year
            IEnumerable<PredefensePeriod> periods = _database.PredefensePeriods.Get(
                new FilterExpression<PredefensePeriod>[]
                {
                    new FilterExpression<PredefensePeriod>(pp => pp.DegreeId == degreeId && pp.GraduationYear == graduationYear)
                },
                null,
                null,
                null,
                new SortExpression<PredefensePeriod, DateTime>(
                    p => p.StartDate,
                    System.ComponentModel.ListSortDirection.Ascending));
            if (periods == null || periods.Count() == 0)
            {
                throw new NoEntityInDatabaseException("Не существует расписания для заданного образовательного уровня и года выпуска.");
            }

            foreach (var period in periods)
            {
                predefenseSchedules.AddRange(GetPredefenseScheduleForPeriod(period.Id));
            }

            return predefenseSchedules;
        }

        public IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int predefensePeriodId)
        {
            return GetPredefenseScheduleForPeriod(predefensePeriodId);
        }

        public IEnumerable<PredefenseSchedule> GetTeacherPredefenseSchedule(int teacherId, int predefensePeriodId)
        {
            // Get teacher appointments, firstly recieve newer dates, that have sorted predefense by time 
            IEnumerable<Appointment> appointments = _database.Appointments.Get(
                new FilterExpression<Appointment>[]
                {
                    new FilterExpression<Appointment>(ap =>
                    ap.TeacherId == teacherId && ap.PredefenseDate.PredefensePeriodId == predefensePeriodId)
                },
                new IncludeExpression<Appointment>[]
                {
                    new IncludeExpression<Appointment>(ap => ap.PredefenseDate)
                },
                null,
                null,
                new SortExpression<Appointment, DateTime>[]
                {
                    new SortExpression<Appointment, DateTime>(
                        ap => ap.PredefenseDate.Date,
                        System.ComponentModel.ListSortDirection.Descending),
                    new SortExpression<Appointment, DateTime>(
                        ap => ap.PredefenseDate.BeginTime,
                        System.ComponentModel.ListSortDirection.Ascending)
                });

            List<PredefenseSchedule> schedules = new List<PredefenseSchedule>();

            foreach (var app in appointments)
            {
                schedules.Add(ConvertToPredefenseSchedule(app.PredefenseDate));
            }
            return schedules;
        }

        private List<PredefenseSchedule> GetPredefenseScheduleForPeriod(int periodId)
        {
            List<PredefenseSchedule> schedules = new List<PredefenseSchedule>(); 

            // Get predefense dates, ordered by date             
            SortExpression<PredefenseDate, DateTime>[] sorts = new SortExpression<PredefenseDate, DateTime>[]
            {
                    new SortExpression<PredefenseDate, DateTime>(pd => pd.Date, System.ComponentModel.ListSortDirection.Ascending),
                    new SortExpression<PredefenseDate, DateTime>(pd => pd.BeginTime, System.ComponentModel.ListSortDirection.Ascending)
            };
            FilterExpression<PredefenseDate>[] filters = new FilterExpression<PredefenseDate>[]
            {
                    new FilterExpression<PredefenseDate>(pd => pd.PredefensePeriodId == periodId)
            };
            IEnumerable<PredefenseDate> predefenseDates = _database.PredefenseDates.Get(
                filters,
                null,
                null,
                null,
                sorts);

            foreach(var date in predefenseDates)
            {
                schedules.Add(ConvertToPredefenseSchedule(date));
            }

            return schedules;
        }

        private PredefenseSchedule ConvertToPredefenseSchedule(PredefenseDate date)
        {            
            PredefenseSchedule schedule = new PredefenseSchedule()
            {
                Teachers = new List<TeacherDTO>()
            };

            // Create predefense date DTO
            PredefenseDateDTO dateDTO = new PredefenseDateDTO()
                {
                Id = date.Id,
                Date = date.Date,
                BeginTime = date.BeginTime,
                FinishTime = date.FinishTime,
                PredefensePeriodId = date.PredefensePeriodId,
                Predefenses = new List<PredefenseDTO>()
            };

            // Get predefenses ordered by time with student and group
            IncludeExpression<Predefense>[] inc = new IncludeExpression<Predefense>[]
            {
                new IncludeExpression<Predefense>(p => p.Student),
                new IncludeExpression<Predefense>(p => p.Student.Group),
                new IncludeExpression<Predefense>(p => p.Student.PeopleNames),
            };
            SortExpression<Predefense, DateTime>[] sor = new SortExpression<Predefense, DateTime>[]
            {
                new SortExpression<Predefense, DateTime>(p => p.Time, System.ComponentModel.ListSortDirection.Ascending),
            };
            FilterExpression<Predefense>[] filt = new FilterExpression<Predefense>[]
            {
                new FilterExpression<Predefense>(p => p.PredefenseDateId == date.Id)
            };
            IEnumerable<Predefense> predefenses = _database.Predefenses.Get(
                filt, inc, null, null, sor);

            // Add predefenses to predefense date
            foreach (var pr in predefenses)
            {
                dateDTO.Predefenses.Add(Mapper.Map<Predefense, PredefenseDTO>(pr));
            }

            // Get teachers of predefense date
            IEnumerable<Appointment> appointments = _database.Appointments.Get(
                new FilterExpression<Appointment>(ap => ap.PredefenseDateId == date.Id),
                new IncludeExpression<Appointment>[]
            {
                new IncludeExpression<Appointment>(app => app.Teacher.PeopleNames)
            });
            foreach (var app in appointments)
            {
                schedule.Teachers.Add(Mapper.Map<Teacher, TeacherDTO>(app.Teacher));
            }

            schedule.PredefenseDate = dateDTO;
                
            return schedule;            
        }
    }
}
