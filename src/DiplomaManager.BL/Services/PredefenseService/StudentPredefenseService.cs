using DiplomaManager.BLL.Interfaces.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Utils;
using DiplomaManager.DAL.Entities.StudentEntities;
using AutoMapper;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.DAL.Entities.PredefenseEntities;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    public class StudentPredefenseService : IStudentPredefenseService
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public StudentPredefenseService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

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

        public IEnumerable<StudentDTO> GetFreeStudents(int groupId)
        {
            // Check if group not exist
            {
                Group group = _database.Groups.Get(groupId);
                if (group == null)
                {
                    throw new NoEntityInDatabaseException("Не найдено группы.");
                }
            }

            IncludeExpression<Student>[] includePath = new IncludeExpression<Student>[] {
                new IncludeExpression<Student>(s => s.PeopleNames),
                new IncludeExpression<Student>(s => s.Group)
            };

            // Sort by name alphabetically
            SortExpression<Student, string>[] sortExpressions =
                new SortExpression<Student, string>[3];
            sortExpressions[0] = new SortExpression<Student, string>(
                p => p.PeopleNames.Where(pn => pn.Locale.Name == _cultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.LastName).FirstOrDefault().Name,
                System.ComponentModel.ListSortDirection.Ascending);
            sortExpressions[1] = new SortExpression<Student, string>(
                p => p.PeopleNames.Where(pn => pn.Locale.Name == _cultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.FirstName).FirstOrDefault().Name,
                System.ComponentModel.ListSortDirection.Ascending);
            sortExpressions[2] = new SortExpression<Student, string>(
                p => p.PeopleNames.Where(pn => pn.Locale.Name == _cultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.Patronymic).FirstOrDefault().Name,
                System.ComponentModel.ListSortDirection.Ascending);

            // Filters for searching free students - students don't have predefenses, or all their predefenses are failed
            FilterExpression<Student>[] filters = new FilterExpression<Student>[] {
                new FilterExpression<Student>(s => s.GroupId == groupId),
                new FilterExpression<Student>(s =>
                    (s.Predefenses.Count == 0) || (s.Predefenses.All(p => p.Passed == false))),
            };

            IEnumerable<Student> students = _database.Students.Get(
                filters, 
                includePath, 
                null, 
                null, 
                sortExpressions);

            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(students);
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

            foreach(var year in degree.Groups.GroupBy(g => g.GraduationYear))
            {
                years.Add(year.Key);
            }

            return years;
        }

        public IEnumerable<GroupDTO> GetGroups(int degreeId, int graduationYear)
        {
            // Check if exist degree
            {
                if (_database.Degrees.Get(degreeId) == null)
                {
                    throw new NoEntityInDatabaseException("Не найдено образовательного уровня.");
                }
            }

            FilterExpression<Group>[] filters = new FilterExpression<Group>[]
                { new FilterExpression<Group>(gr => gr.GraduationYear == graduationYear && gr.DegreeId == degreeId)};
            SortExpression<Group, string>[] sortExpressions = new SortExpression<Group, string>[]
                { new SortExpression<Group, string>(gr => gr.Name, System.ComponentModel.ListSortDirection.Ascending) };

            IEnumerable<Group> groups = _database.Groups.Get(filters, null, null, null, sortExpressions);
            if (groups == null || groups.Count() == 0)
            {
                // throw new NoEntityInDatabaseException("Нет групп в заданном году выпуска образовательного уровня.");
            }
            return Mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(groups);
        }

        public IEnumerable<PredefenseDateDTO> GetPredefenseSchedule(int degreeId, int graduationYear)
        {
            // Check if exist degree
            {
                if (_database.Degrees.Get(degreeId) == null)
                {
                    throw new NoEntityInDatabaseException("Не найдено образовательного уровня.");
                }
            }

            // Predefense schedule (result)
            List<PredefenseDateDTO> predefenseSchedule = new List<PredefenseDateDTO>();

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
                // Get predefense dates, ordered by date             
                SortExpression<PredefenseDate, DateTime>[] sorts = new SortExpression<PredefenseDate, DateTime>[]
                {
                    new SortExpression<PredefenseDate, DateTime>(pd => pd.Date, System.ComponentModel.ListSortDirection.Ascending),
                    new SortExpression<PredefenseDate, DateTime>(pd => pd.BeginTime, System.ComponentModel.ListSortDirection.Ascending)
                };
                FilterExpression<PredefenseDate>[] filters = new FilterExpression<PredefenseDate>[]
                {
                    new FilterExpression<PredefenseDate>(pd => pd.PredefensePeriodId == period.Id)
                };
                IEnumerable<PredefenseDate> predefenseDates = _database.PredefenseDates.Get(
                    filters,
                    null,
                    null,
                    null,
                    sorts);

                foreach (var date in predefenseDates)
                {
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
                    // Add predefense date to schedule
                    predefenseSchedule.Add(dateDTO);
                }
            }

            return predefenseSchedule;
        }

        public void SubmitPredefense(int studentId, int predefenseId)
        {            
            Student student = _database.Students.Get(
                new FilterExpression<Student>[]
                {
                    new FilterExpression<Student>(s => s.Id == studentId)
                },
                new IncludeExpression<Student>[]
                {
                    new IncludeExpression<Student>(s => s.Group),
                    new IncludeExpression<Student>(s => s.Predefenses)
                }).FirstOrDefault();
            // Check, if exists student
            if (student == null)
            {
                throw new NoEntityInDatabaseException("Указанного студента не найдено.");
            }
            // Check, if student doesn't have passed predefenses
            if (student.Predefenses.Any(p => p.Passed == true))
            {
                throw new IncorrectActionException("Студент уже прошёл предзащиту.");
            }
            // Check, if student doesn't have active predefenses
            if (student.Predefenses.Any(p => p.Passed == null || p.Passed == true))
            {
                throw new IncorrectActionException("Студент уже записан на предзащиту.");
            }

            Predefense predefense = _database.Predefenses.Get(
                new FilterExpression<Predefense>[]
                {
                    new FilterExpression<Predefense>(p => p.Id == predefenseId)
                },
                new IncludeExpression<Predefense>[]
                {
                    new IncludeExpression<Predefense>(p => p.PredefenseDate.PredefensePeriod)
                }).FirstOrDefault();

            // Check, if exists predefense
            if (predefense == null)
            {
                throw new NoEntityInDatabaseException("Указанной предзащиты не найдено.");
            }

            // Check, if degrees matches
            if (predefense.PredefenseDate.PredefensePeriod.DegreeId != student.Group.DegreeId)
            {
                throw new IncorrectActionException("Образовательные уровни студента и предзащиты не совпадают.");
            }
            // Check, if graduation years matches
            if (predefense.PredefenseDate.PredefensePeriod.GraduationYear != student.Group.GraduationYear)
            {
                throw new IncorrectActionException("Год выпуска студента и предзащиты не совпадают.");
            }

            predefense.StudentId = studentId;
            _database.Save();
        }
    }
}
