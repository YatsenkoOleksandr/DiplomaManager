using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.BLL.Services
{
    public class RequestService : IRequestService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private IEmailService EmailService { get; }
        private ILocaleConfiguration LocaleConfiguration { get; }
        private IUserService UserService { get; }

        public RequestService(IDiplomaManagerUnitOfWork uow, IEmailService emailService, ILocaleConfiguration localeConfiguration,
            IUserService userService)
        {
            Database = uow;
            EmailService = emailService;
            LocaleConfiguration = localeConfiguration;
            UserService = userService;
        }

        public DevelopmentAreaDTO GetDevelopmentArea(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            return Mapper.Map<DevelopmentArea, DevelopmentAreaDTO>(Database.DevelopmentAreas.Get(id));
        }

        public IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas()
        {
            var das = Database.DevelopmentAreas.Get();
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            return Mapper.Map<IEnumerable<DevelopmentArea>, IEnumerable<DevelopmentAreaDTO>>(das);
        }

        public void AddDevelopmentArea(DevelopmentAreaDTO developmentArea)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentArea>());
            Database.DevelopmentAreas.Add(Mapper.Map<DevelopmentAreaDTO, DevelopmentArea>(developmentArea));
            Database.Save();
        }

        public void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentArea>());
            Database.DevelopmentAreas.Update(Mapper.Map<DevelopmentAreaDTO, DevelopmentArea>(developmentArea));
            Database.Save();
        }

        public void DeleteDevelopmentArea(int id)
        {
            Database.DevelopmentAreas.Remove(id);
            Database.Save();
        }

        public IEnumerable<TeacherDTO> GetTeachers(int? daId = null, string cultureName = null)
        {
            List<Teacher> teachers;
            var filterExpressions = new List<FilterExpression<Teacher>>
            { new FilterExpression<Teacher>(t => !(t is Admin)) };
            var includePaths = new List<IncludeExpression<Teacher>>();
            if (daId != null)
            {
                includePaths.Add(new IncludeExpression<Teacher>(t => t.DevelopmentAreas));
                filterExpressions.Add(new FilterExpression<Teacher>(
                    t => t.DevelopmentAreas.Any(da => da.Id == daId.Value)));
            }
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                teachers = Database.Teachers.Get(filterExpressions.ToArray(), includePaths.ToArray()).ToList();

                Database.PeopleNames.Get(
                    new FilterExpression<PeopleName>(f => f.Locale.Name == cultureName), 
                    new[] { new IncludeExpression<PeopleName>(p => p.Locale), new IncludeExpression<PeopleName>(p => p.Users) });
            }
            else
            {
                includePaths.AddRange(new[] 
                {
                    new IncludeExpression<Teacher>(p => p.PeopleNames)
                });
                teachers = Database.Teachers.Get(filterExpressions.ToArray(), includePaths.ToArray()).ToList();
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PeopleName, PeopleNameDTO>();
                cfg.CreateMap<Teacher, TeacherDTO>();
            });

            var teacherDtos = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers);
            return teacherDtos;
        }

        public IEnumerable<DegreeDTO> GetDegrees(string cultureName = null)
        {
            List<Degree> degrees;
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                degrees = Database.Degrees.Get().ToList();
                Database.DegreeNames.Get(new FilterExpression<DegreeName>(dn => dn.Locale.Name == cultureName), 
                    new[] { new IncludeExpression<DegreeName>(d => d.Locale) });
            }
            else
            {
                degrees = Database.Degrees.Get(new IncludeExpression<Degree>(d => d.DegreeNames)).ToList();
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Degree, DegreeDTO>();
                cfg.CreateMap<DegreeName, DegreeNameDTO>();
                cfg.CreateMap<Capacity, CapacityDTO>();
            });

            var degreeDtos = Mapper.Map<IEnumerable<Degree>, IEnumerable<DegreeDTO>>(degrees);
            return degreeDtos;
        }

        public CapacityDTO GetCapacity(int degreeId, int teacherId)
        {
            var capacity = Database.Capacities
                .Get(new FilterExpression<Capacity>(c => c.DegreeId == degreeId && c.TeacherId == teacherId))
                .SingleOrDefault();
            Mapper.Initialize(cfg => cfg.CreateMap<Capacity, CapacityDTO>());
            var capacityDto = Mapper.Map<Capacity, CapacityDTO>(capacity);
            return capacityDto;
        }

        public IEnumerable<GroupDTO> GetGroups(int degreeId)
        {
            var groups = Database.Groups.Get(new FilterExpression<Group>(g => g.DegreeId == degreeId));
            Mapper.Initialize(cfg => cfg.CreateMap<Group, GroupDTO>());
            var groupDtos = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(groups);
            return groupDtos;
        }

        public void CreateDiplomaRequest(StudentDTO studentDto, int daId, int teacherId, int localeId, string title)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PeopleNameDTO, PeopleName>();
                cfg.CreateMap<GroupDTO, Group>();
                cfg.CreateMap<StudentDTO, Student>();
            });

            var studentRes = GetStudent(studentDto);

            var project = new Project
            {
                Student = studentRes,
                TeacherId = teacherId,
                CreationDate = DateTime.Now,
                DevelopmentAreaId = daId
            };
            Database.Projects.Add(project);

            var projTitle = new ProjectTitle { Project = project, Title = title, CreationDate = DateTime.Now, LocaleId = localeId };
            Database.ProjectTitles.Add(projTitle);

            Database.Save();
            EmailService.SendEmailAsync("teland94@mail.ru", "Test", "Test!");
        }

        public IEnumerable<PeopleNameDTO> GetStudentNames(string query, NameKindDTO nameKindDto, int maxItems = 10)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<NameKindDTO, NameKind>();
            });
            var nameKind = Mapper.Map<NameKindDTO, NameKind>(nameKindDto);

            var filters = new[]
            {
                new FilterExpression<PeopleName>(n => n.NameKind == nameKind),
                new FilterExpression<PeopleName>(n => string.IsNullOrEmpty(query) || n.Name.Contains(query)),
                new FilterExpression<PeopleName>(n => n.Locale.Name == LocaleConfiguration.DefaultLocaleName)
            };

            var nameConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NameKind, NameKindDTO>();
                cfg.CreateMap<PeopleName, PeopleNameDTO>();
            });
            var nameMapper = nameConfiguration.CreateMapper();

            var names = Database.PeopleNames.Get(filters, 
                new[] { new IncludeExpression<PeopleName>(n => n.Locale) },
                pageSize: maxItems,
                sortExpressions: new SortExpression<PeopleName, string>(n => n.Name, ListSortDirection.Ascending));

            var namesDto = nameMapper.Map<IEnumerable<PeopleName>, IEnumerable<PeopleNameDTO>>(names);
            return namesDto;
        }

        private Student GetStudent(StudentDTO studentDto)
        {
            var nameConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NameKindDTO, NameKind>();
                cfg.CreateMap<PeopleNameDTO, PeopleName>();
            });
            var nameMapper = nameConfiguration.CreateMapper();
            var namesDto = nameMapper.Map<IEnumerable<PeopleNameDTO>, IEnumerable<PeopleName>>(studentDto.PeopleNames);

            var studentRes = UserService.GetUserFromFullName<Student>(namesDto.ToList());
            if (studentRes == null)
                throw new InvalidOperationException("Can't get Student");

            if (string.IsNullOrWhiteSpace(studentRes.Email) || string.IsNullOrWhiteSpace(studentRes.Login))
            {
                studentRes.Login = studentDto.Email;
                studentRes.Email = studentDto.Email;
                Database.Students.Update(studentRes);
            }
            return studentRes;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
