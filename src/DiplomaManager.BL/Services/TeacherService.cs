using System.Collections.Generic;
using AutoMapper;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.ProjectEntities;
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

        public TeacherService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId, string cultureName = null)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FirstName, FirstNameDTO>();
                cfg.CreateMap<LastName, LastNameDTO>();
                cfg.CreateMap<Patronymic, PatronymicDTO>();

                cfg.CreateMap<Teacher, TeacherDTO>();
                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<Student, StudentDTO>();

                cfg.CreateMap<ProjectTitle, ProjectTitleDTO>();
                cfg.CreateMap<Project, ProjectDTO>();
            });

            var filterExprs = new List<FilterExpression<Project>>
            {
                new FilterExpression<Project>(p => p.TeacherId == teacherId)
            };
            var includePaths = new List<IncludeExpression<Project>>
            {
                new IncludeExpression<Project>(p => p.Student),
                new IncludeExpression<Project>(p => p.Student.Group)
            };

            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                Database.FirstNames.Get(
                    f => f.Locale.Name == cultureName, new[] {new IncludeExpression<FirstName>(p => p.Locale)});
                Database.LastNames.Get(
                    l => l.Locale.Name == cultureName, new[] {new IncludeExpression<LastName>(p => p.Locale)});
                Database.Patronymics.Get(
                    p => p.Locale.Name == cultureName, new[] {new IncludeExpression<Patronymic>(p => p.Locale)});

                Database.ProjectTitles.Get(
                    t => t.Locale.Name == cultureName, new[] {new IncludeExpression<ProjectTitle>(p => p.Locale)});
            }
            else
            {
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.FirstNames));
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.LastNames));
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.Patronymics));

                includePaths.Add(new IncludeExpression<Project>(p => p.ProjectTitles));
            }

            var projects = Database.Projects.Get(filters: filterExprs.ToArray(), includePaths: includePaths.ToArray());
            var projectDtos = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);
            return projectDtos;
        }
    }
}