using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private ILocaleConfiguration CultureConfiguration { get; }

        public TeacherService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration)
        {
            Database = uow;
            CultureConfiguration = configuration;
        }

        public IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId)
        {
            CreateProjectMapping();

            var filterExprs = new List<FilterExpression<Project>>
            {
                new FilterExpression<Project>(p => p.TeacherId == teacherId)
            };
            var includePaths = new List<IncludeExpression<Project>>
            {
                new IncludeExpression<Project>(p => p.Student),
                new IncludeExpression<Project>(p => p.Student.Group)
            };

            if (!string.IsNullOrWhiteSpace(CultureConfiguration.DefaultLocaleName))
            {
                Database.FirstNames.Get(
                    new FilterExpression<FirstName>(f => f.Locale.Name == CultureConfiguration.DefaultLocaleName), new[] {new IncludeExpression<FirstName>(p => p.Locale)});
                Database.LastNames.Get(
                    new FilterExpression<LastName>(l => l.Locale.Name == CultureConfiguration.DefaultLocaleName), new[] {new IncludeExpression<LastName>(p => p.Locale)});
                Database.Patronymics.Get(
                    new FilterExpression<Patronymic>(p => p.Locale.Name == CultureConfiguration.DefaultLocaleName), new[] {new IncludeExpression<Patronymic>(p => p.Locale)});
            }
            else
            {
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.FirstNames));
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.LastNames));
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.Patronymics));
            }

            var cultureFilterExprs = CultureConfiguration.LocaleNames.Select(c =>
                new FilterExpression<ProjectTitle>(p => p.Locale.Name == c)).ToArray();
            Database.ProjectTitles.Get(
                filters: cultureFilterExprs, includePaths: new[] { new IncludeExpression<ProjectTitle>(p => p.Locale) });
            includePaths.Add(new IncludeExpression<Project>(p => p.ProjectTitles.Select(t => t.Locale)));

            var projects = Database.Projects.Get(filters: filterExprs.ToArray(), includePaths: includePaths.ToArray());
            var projectDtos = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);
            return projectDtos;
        }

        public void EditDiplomaProjectTitles(IEnumerable<ProjectEditTitle> projectTitles)
        {
            foreach (var projectTitle in projectTitles)
            {
                var pTitle = Database.ProjectTitles.Get(projectTitle.Id);
                pTitle.Title = projectTitle.Title;
                Database.ProjectTitles.Update(pTitle);
            }
            Database.Save();
        }

        private static void CreateProjectMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FirstName, FirstNameDTO>();
                cfg.CreateMap<LastName, LastNameDTO>();
                cfg.CreateMap<Patronymic, PatronymicDTO>();

                cfg.CreateMap<Teacher, TeacherDTO>();
                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<Student, StudentDTO>();

                cfg.CreateMap<Locale, LocaleDTO>();
                cfg.CreateMap<ProjectTitle, ProjectTitleDTO>();
                cfg.CreateMap<Project, ProjectDTO>();
            });
        }
    }

    public class ProjectEditTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}