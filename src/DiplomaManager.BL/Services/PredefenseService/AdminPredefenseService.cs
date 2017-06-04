using AutoMapper;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
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
    public class AdminPredefenseService : IAdminPredefenseService
    {
        private const int WORKING_MINUTES = 540;

        private const int MAXIMUM_TEACHERS = 3;

        private const int MAXIMUM_TEACHER_VISITS = 5;

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

        private void CheckAbilityToCarryOutPredefensePeriod(PredefensePeriodDTO period)
        {
            int studentsPerDay = WORKING_MINUTES / (int)period.PredefenseStudentTime.TotalMinutes;

            int studentCount = _database.Students.Count(
                new FilterExpression<Student>[]
                {
                    new FilterExpression<Student>(s =>
                        s.Group.GraduationYear == period.GraduationYear &&
                        s.Group.DegreeId == period.DegreeId)
                });
            if (studentCount == 0)
            {
                throw new NoEntityInDatabaseException("Не найдено студентов указанного образовательного уровня и года выпуска.");
            }

            DateTime currentDate = period.StartDate;

            // Add days if period starts from saturday or sunday
            if (currentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                currentDate = currentDate.AddDays(2);
            }
            if (currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                currentDate = currentDate.AddDays(1);
            }

            while (studentCount > 0)
            {
                if (currentDate > period.FinishDate)
                {
                    throw new IncorrectParameterException("Не хватит выделенного периода для проведения предзащит студентов.");
                }

                studentCount -= studentsPerDay;
                if (currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    currentDate = currentDate.AddDays(2);
                }
                currentDate = currentDate.AddDays(1);
            }
        }

        private void CheckAbilityToCarryOutPredefenseDate(PredefensePeriod period, PredefenseDateDTO date)
        {
            if (date.Date < period.StartDate)
                throw new IncorrectParameterException("Дата проведения предзащит наступает раньше начала периода.");
            if (date.Date > period.FinishDate)
                throw new IncorrectParameterException("Дата проведения предзащит наступает позже окончания периода.");

            int students = (int)(date.FinishTime - date.BeginTime).TotalMinutes /
                (int)period.PredefenseStudentTime.TotalMinutes;
            if (students == 0)
                throw new IncorrectParameterException("Не хватит выделенного времени для проведения предзащит.");
        }

        #endregion

        public IEnumerable<DegreeDTO> GetDegrees()
        {
            IncludeExpression<Degree> includePath = new IncludeExpression<Degree>(d => d.DegreeNames);

            IEnumerable<Degree> degrees = _database.Degrees.Get(includePath);

            if (degrees == null || degrees.Count() == 0)
            {
                throw new NoEntityInDatabaseException("Не найдено образовательных уровней.");
            }

            return Mapper.Map<IEnumerable<Degree>, IEnumerable<DegreeDTO>>(degrees);
        }

        public IEnumerable<int> GetGraduationYears(int degreeId)
        {
            Degree degree = _database.Degrees.Get(
                new FilterExpression<Degree>(d => d.Id == degreeId),
                new IncludeExpression<Degree>[]
                { new IncludeExpression<Degree>(d => d.Groups) }).FirstOrDefault();

            if (degree == null)
            {
                throw new NoEntityInDatabaseException("Не найден образовательный уровень.");
            }
            if (degree.Groups == null || degree.Groups.Count == 0)
            {
                // throw new NoEntityInDatabaseException("Нет выпусков образовательного уровня.");
            }

            List<int> years = new List<int>();

            foreach (var year in degree.Groups.GroupBy(g => g.GraduationYear))
            {
                years.Add(year.Key);
            }
            return years;
        }

        public IEnumerable<PredefensePeriodDTO> GetPredefensePeriods()
        {
            // Get info about predefense period - without predefense dates, with degrees
            // sorted by date
            IEnumerable<PredefensePeriod> periods = _database.PredefensePeriods.Get(
                null,
                new IncludeExpression<PredefensePeriod>[]
                {
                    new IncludeExpression<PredefensePeriod>(p => p.Degree.DegreeNames)
                },
                null,
                null,
                new SortExpression<PredefensePeriod, DateTime>[]
                {
                    new SortExpression<PredefensePeriod, DateTime>(pd => pd.StartDate, ListSortDirection.Descending)
                });
            return Mapper.Map<IEnumerable<PredefensePeriod>, IEnumerable<PredefensePeriodDTO>>(periods);
        }

        public IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int predefensePeriodId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefensePeriodExistance(predefensePeriodId);
            }
            PredefenseScheduler scheduler = new PredefenseScheduler(_database);
            return scheduler.GetPredefenseSchedule(predefensePeriodId);
        }

        public PredefenseDTO GetPredefense(int predefenseId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefenseExistance(predefenseId);
            }

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

        public IEnumerable<PredefenseTeacherCapacityDTO> GetPredefensePeriodTeachers(int predefensePeriodId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefensePeriodExistance(predefensePeriodId);
            }

            // Get capacities of period with teacher names
            IEnumerable<PredefenseTeacherCapacity> teachers = _database.PredefenseTeacherCapacities.Get(
                new FilterExpression<PredefenseTeacherCapacity>(ptc => ptc.PredefensePeriodId == predefensePeriodId),
                new IncludeExpression<PredefenseTeacherCapacity>[]
                {
                    new IncludeExpression<PredefenseTeacherCapacity>(ptc => ptc.Teacher),
                    new IncludeExpression<PredefenseTeacherCapacity>(ptc => ptc.Teacher.PeopleNames)
                });
            return Mapper.Map<IEnumerable<PredefenseTeacherCapacity>, IEnumerable<PredefenseTeacherCapacityDTO>>(
                teachers);
        }

        public IEnumerable<TeacherDTO> GetPredefenseDateTeachers(int predefenseDateId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefenseDateExistance(predefenseDateId);
            }            
            List<TeacherDTO> teachers = new List<TeacherDTO>();
            IEnumerable<Appointment> appointments = _database.Appointments.Get(
                new FilterExpression<Appointment>(app => app.PredefenseDateId == predefenseDateId),
                new IncludeExpression<Appointment>[]
                {
                    new IncludeExpression<Appointment>(app => app.Teacher.PeopleNames)
                });
            foreach(var ap in appointments)
            {
                teachers.Add(Mapper.Map<Teacher, TeacherDTO>(ap.Teacher));
            }
            return teachers;
        }

        public IEnumerable<TeacherDTO> GetFreeTeachersToPeriod(int predefensePeriodId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefensePeriodExistance(predefensePeriodId);
            }
            // Get teachers without predefense capcity to current period with their names
            IEnumerable<Teacher> freeTeachers = _database.Teachers.Get(
                new FilterExpression<Teacher>(
                    t => t.PredefenseTeacherCapacities.All(ptc => ptc.PredefensePeriodId != predefensePeriodId)),
                new IncludeExpression<Teacher>[]
                {
                    new IncludeExpression<Teacher>(t => t.PeopleNames)
                });

            return Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(freeTeachers);
        }

        public IEnumerable<TeacherDTO> GetFreeTeachersToPredefenseDate(int predefenseDateId)
        {
            PredefenseDate predefenseDate = _database.PredefenseDates.Get(predefenseDateId);
            if (predefenseDate == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного дня проведения предзащиты.");
            }

            // Teachers of period without appointments on predefenseDate
            IEnumerable<Teacher> teachers = _database.Teachers.Get(
                new FilterExpression<Teacher>[]
                {
                    new FilterExpression<Teacher>(t => t.PredefenseTeacherCapacities.Any(
                        ptc => ptc.PredefensePeriodId == predefenseDate.PredefensePeriodId)),
                    new FilterExpression<Teacher>(t => t.Appointments.All(app => app.PredefenseDateId != predefenseDateId))
                },
                new IncludeExpression<Teacher>[]
                {
                    new IncludeExpression<Teacher>(t => t.PredefenseTeacherCapacities),
                    new IncludeExpression<Teacher>(t => t.Appointments)
                });

            // Add to list teachers that have free time
            List<TeacherDTO> freeTeachers = new List<TeacherDTO>();
            foreach (Teacher teach in teachers)
            {
                var cap = teach.PredefenseTeacherCapacities.Where(ptc => ptc.PredefensePeriodId == predefenseDate.PredefensePeriodId)
                    .FirstOrDefault();
                if (cap.Count < cap.Total)
                {
                    // Find if teacher don't have another predefenses that day
                    IEnumerable<Appointment> teacherAppointments = _database.Appointments.Get(
                        new FilterExpression<Appointment>(ap => ap.TeacherId == teach.Id &&
                            ap.PredefenseDate.Date == predefenseDate.Date),
                        new IncludeExpression<Appointment>[]
                        {
                            new IncludeExpression<Appointment>(ap => ap.PredefenseDate)
                        });

                    if (teacherAppointments.Count() > 0)
                    {
                        bool teachIsFree = true;
                        foreach (var app in teacherAppointments)
                        {
                            if (!(app.PredefenseDate.FinishTime < predefenseDate.BeginTime ||
                                app.PredefenseDate.BeginTime > predefenseDate.FinishTime))
                            {
                                teachIsFree = false;
                                break;
                            }
                        }

                        if (teachIsFree)
                        {
                            freeTeachers.Add(Mapper.Map<Teacher, TeacherDTO>(teach));
                        }
                    }
                    else
                    {
                        freeTeachers.Add(Mapper.Map<Teacher, TeacherDTO>(teach));
                    }
                }
            }
            return freeTeachers;
        }

        public IEnumerable<StudentDTO> GetFreeStudents(int predefensePeriodId)
        {
            PredefensePeriod period = _database.PredefensePeriods.Get(predefensePeriodId);
            if (period == null)
            {
                throw new NoEntityInDatabaseException("Не найден указанный период предзащит.");
            }

            IEnumerable<Student> freeStudents = _database.Students.Get(
                new FilterExpression<Student>[]
                {
                    // Students with same degree and graduation year
                    new FilterExpression<Student>(
                        s => s.Group.DegreeId == period.DegreeId && s.Group.GraduationYear == period.GraduationYear),
                    // Students that don't have predefenses or all predefenses are failed
                    new FilterExpression<Student>(
                        s => s.Predefenses.Count == 0 || s.Predefenses.All(p => p.Passed == false))
                },
                new IncludeExpression<Student>[]
                {
                    new IncludeExpression<Student>(s => s.Group),
                    new IncludeExpression<Student>(s => s.PeopleNames)
                });

            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(freeStudents);
        }

        public void SavePredefense(PredefenseDTO predefense)
        {
            Predefense databasePredefense = _database.Predefenses.Get(predefense.Id);
            if (databasePredefense == null)
            {
                throw new NoEntityInDatabaseException("Не найдена указанная предзащита.");
            }

            databasePredefense.ControlSigned = predefense.ControlSigned;
            databasePredefense.EconomySigned = predefense.EconomySigned;
            databasePredefense.Passed = predefense.Passed;
            databasePredefense.ReportExist = predefense.ReportExist;
            databasePredefense.SafetySigned = predefense.SafetySigned;

            if (predefense.PresentationReadiness >= 0 && predefense.PresentationReadiness <= 100)
                databasePredefense.PresentationReadiness = predefense.PresentationReadiness;

            if (predefense.SoftwareReadiness >= 0 && predefense.SoftwareReadiness <= 100)
                databasePredefense.SoftwareReadiness = predefense.SoftwareReadiness;

            if (predefense.WritingReadiness >= 0 && predefense.WritingReadiness <= 100)
                databasePredefense.WritingReadiness = predefense.WritingReadiness;

            _database.Predefenses.Update(databasePredefense);
            _database.Save();
        }


        public void CreatePredefensePeriod(PredefensePeriodDTO predefensePeriod)
        {
            // Check if degree exists
            if (_database.Degrees.Get(predefensePeriod.DegreeId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного образовательного уровня.");
            }
            // Check if groups exist
            int groupCounts = _database.Groups.Count(new FilterExpression<Group>[]
            {
                new FilterExpression<Group>(g => g.DegreeId == predefensePeriod.DegreeId &&
                    g.GraduationYear == predefensePeriod.GraduationYear)
            });
            if (groupCounts == 0)
            {
                throw new NoEntityInDatabaseException("Не найдено групп студентов с указанными образовательным уровнем и годом выпуска.");
            }

            // Check if dates are corres
            if (predefensePeriod.StartDate.Date > predefensePeriod.FinishDate.Date)
            {
                throw new IncorrectParameterException("Дата начала периода больше даты окончания периода.");
            }
            if (predefensePeriod.PredefenseStudentTime.TotalMinutes < 0 ||
                predefensePeriod.PredefenseStudentTime.TotalMinutes > 120)
            {
                throw new IncorrectParameterException("Выделенное время студенту должно быть не больше 120 минут.");
            }

            // Check if period can be carry out
            CheckAbilityToCarryOutPredefensePeriod(predefensePeriod);

            // Create period
            PredefensePeriod databasePeriod = new PredefensePeriod()
            {
                DegreeId = predefensePeriod.DegreeId,
                GraduationYear = predefensePeriod.GraduationYear,
                PredefenseStudentTime = predefensePeriod.PredefenseStudentTime,
                StartDate = predefensePeriod.StartDate,
                FinishDate = predefensePeriod.FinishDate
            };
            _database.PredefensePeriods.Add(databasePeriod);
            _database.Save();
        }

        public void DeletePredefensePeriod(int predefensePeriodId)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckPredefensePeriodExistance(predefensePeriodId);
            }

            // Remove only from table "PredefensePeriod" - other entities will be removed automatically
            _database.PredefensePeriods.Remove(predefensePeriodId);
            _database.Save();
        }

        public void EditPredefensePeriod(PredefensePeriodDTO predefensePeriod)
        {
            throw new NotImplementedException();
        }


        public void CreatePredefenseDate(PredefenseDateDTO predefenseDate)
        {
            if (predefenseDate.BeginTime >= predefenseDate.FinishTime)
            {
                throw new IncorrectParameterException("Время начала проведения предзащит больше времени окончания проведения.");
            }

            PredefensePeriod period = _database.PredefensePeriods.Get(predefenseDate.PredefensePeriodId);
            if (period == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного периода проведения предзащит.");
            }
            CheckAbilityToCarryOutPredefenseDate(period, predefenseDate);

            PredefenseDate date = new PredefenseDate()
            {
                BeginTime = predefenseDate.BeginTime,
                Date = predefenseDate.Date,
                FinishTime = predefenseDate.FinishTime
            };
            _database.PredefenseDates.Add(date);

            DateTime predefenseTime = predefenseDate.BeginTime;
            while (predefenseTime < predefenseDate.FinishTime)
            {
                Predefense predefense = new Predefense()
                {
                    PredefenseDate = date,
                    Time = predefenseTime,
                    Passed = null,
                    StudentId = null
                };
                _database.Predefenses.Add(predefense);
                predefenseTime = predefenseTime.Add(period.PredefenseStudentTime);
            }
            _database.Save();
        }

        public void DeletePredefenseDate(int predefenseDateId)
        {
            PredefenseDate date = _database.PredefenseDates.Get(predefenseDateId);
            if (date == null)
            {
                throw new NoEntityInDatabaseException("Не найден указанный день проведения предзащит.");
            }

            // Get teachers who have to attend predefense date
            IEnumerable<Teacher> teachers = _database.Teachers.Get(
                new FilterExpression<Teacher>(t => t.Appointments.Any(ap => ap.PredefenseDateId == predefenseDateId)),
                new IncludeExpression<Teacher>[]
                {
                    new IncludeExpression<Teacher>(t => t.PredefenseTeacherCapacities)
                });
            // Update predefense capacity
            foreach (var t in teachers)
            {
                var cap = t.PredefenseTeacherCapacities.Where(ptc => ptc.PredefensePeriodId == date.PredefensePeriodId)
                    .FirstOrDefault();
                cap.Count--;
                _database.PredefenseTeacherCapacities.Update(cap);
            }
            // Delete only date - appointments will be deleted automatically
            _database.PredefenseDates.Remove(predefenseDateId);
            _database.Save();
        }

        public void EditPredefenseDate(PredefensePeriodDTO predefensePeriod)
        {
            throw new NotImplementedException();
        }


        public void CreatePredefenseTeacher(PredefenseTeacherCapacityDTO teacherCapacity)
        {
            {
                PredefenseChecker checker = new PredefenseChecker(_database);
                checker.CheckTeacherExistance(teacherCapacity.TeacherId);
                checker.CheckPredefensePeriodExistance(teacherCapacity.PredefensePeriodId);

                PredefenseTeacherCapacity cap = _database.PredefenseTeacherCapacities.Get(
                    new FilterExpression<PredefenseTeacherCapacity>(ptc =>
                    ptc.PredefensePeriodId == teacherCapacity.PredefensePeriodId && ptc.TeacherId == teacherCapacity.TeacherId))
                    .FirstOrDefault();
                if (cap != null)
                {
                    throw new IncorrectActionException("Преподаватель уже должен посещать предзащиты в данном периоде.");
                }
            }

            if (teacherCapacity.Total <= 0 || teacherCapacity.Total > MAXIMUM_TEACHER_VISITS)
            {
                throw new IncorrectParameterException("Количество посещений не корректно.");
            }

            PredefenseTeacherCapacity capacity = new PredefenseTeacherCapacity()
            {
                TeacherId = teacherCapacity.TeacherId,
                PredefensePeriodId = teacherCapacity.PredefensePeriodId,
                Total = teacherCapacity.Total,
                Count = 0
            };

            _database.PredefenseTeacherCapacities.Add(capacity);
            _database.Save();
        }

        public void DeleteTeacher(int teacherId, int predefensePeriodId)
        {            
            PredefenseChecker checker = new PredefenseChecker(_database);
            checker.CheckTeacherExistance(teacherId);
            checker.CheckPredefensePeriodExistance(predefensePeriodId);

            PredefenseTeacherCapacity cap = _database.PredefenseTeacherCapacities.Get(
                new FilterExpression<PredefenseTeacherCapacity>(ptc =>
                ptc.PredefensePeriodId == predefensePeriodId && ptc.TeacherId == teacherId))
                .FirstOrDefault();
            if (cap == null)
            {
                throw new IncorrectActionException("Преподаватель не обязан был посещать предзащиты в данном периоде.");
            }

            // Delete teacher appointments
            IEnumerable<Appointment> teacherAppointments = _database.Appointments.Get(
                new FilterExpression<Appointment>[]
                {
                    new FilterExpression<Appointment>(app => app.TeacherId == teacherId),
                    new FilterExpression<Appointment>(app => app.PredefenseDate.PredefensePeriodId == predefensePeriodId)
                });
            
            if (teacherAppointments != null || teacherAppointments.Count() != 0)
            {
                foreach(var app in teacherAppointments)
                {
                    _database.Appointments.Remove(app.Id);
                }
            }

            // Delete teacher predefense capacity
            _database.PredefenseTeacherCapacities.Remove(cap.Id);

            _database.Save();
        }

        public void EditTeacherCapacity(PredefenseTeacherCapacityDTO capacity)
        {
            PredefenseTeacherCapacity _databaseCap = _database.PredefenseTeacherCapacities.Get(capacity.Id);
            if (_databaseCap == null)
            {
                throw new NoEntityInDatabaseException("Не найдено назначения преподавателя посещать предзащиты периода.");
            }

            if (capacity.Total < 0 || capacity.Total > MAXIMUM_TEACHER_VISITS)
            {
                throw new IncorrectParameterException("Количество посещений не корректно.");
            }

            if (_databaseCap.Count > capacity.Total)
            {
                throw new IncorrectParameterException("Преподаватель записан на большее количество дат предзащит.");
            }

            _databaseCap.Total = capacity.Total;
            _database.PredefenseTeacherCapacities.Update(_databaseCap);
            _database.Save();            
        }


        public void SubmitStudentToPredefense(int predefenseId, int studentId)
        {
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.SubmitStudentToPredefense(studentId, predefenseId);
        }

        public void DenySubmitStudent(int predefenseId, int studentId)
        {
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.DenyStudentSubmitToPredefense(studentId, predefenseId);
        }

        public void SubmitTeacherToPredefenseDate(int predefenseDateId, int teacherId)
        {
            PredefenseChecker checker = new PredefenseChecker(_database);
            checker.CheckTeacherExistance(teacherId);
            checker.CheckPredefenseDateExistance(predefenseDateId);
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.SubmitTeacherToPredefenseDate(teacherId, predefenseDateId);
        }

        public void DenySubmitTeacher(int predefenseDateId, int teacherId)
        {
            PredefenseChecker checker = new PredefenseChecker(_database);
            checker.CheckTeacherExistance(teacherId);
            checker.CheckPredefenseDateExistance(predefenseDateId);
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.DenyTeacherSubmitToPredefenseDate(teacherId, predefenseDateId);
        }
    }
}
