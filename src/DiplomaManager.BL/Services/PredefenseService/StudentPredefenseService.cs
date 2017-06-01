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
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.BLL.DTOs.TeacherDTOs;

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

        public IEnumerable<PredefenseSchedule> GetPredefenseSchedule(int degreeId, int graduationYear)
        {
            // Check if exist degree
            {
                if (_database.Degrees.Get(degreeId) == null)
                {
                    throw new NoEntityInDatabaseException("Не найдено образовательного уровня.");
                }
            }

            PredefenseScheduler scheduler = new PredefenseScheduler(_database, _cultureConfiguration, _emailService);

            return scheduler.GetPredefenseSchedule(degreeId, graduationYear);
        }

        public void SubmitPredefense(int studentId, int predefenseId)
        {
            PredefenseSubmitter submitter = new PredefenseSubmitter(_database, _cultureConfiguration, _emailService);
            submitter.SubmitStudentToPredefense(studentId, predefenseId);
        }
    }
}
