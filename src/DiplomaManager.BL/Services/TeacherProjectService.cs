﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class TeacherProjectService : ITeacherProjectService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private ILocaleConfiguration CultureConfiguration { get; }
        private IEmailService EmailService { get; }

        public TeacherProjectService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            Database = uow;
            CultureConfiguration = configuration;
            EmailService = emailService;
        }

        public IEnumerable<ProjectDTO> GetDiplomaRequests(int teacherId, string query = "", int currentPage = 1, int itemsPerPage = 10)
        {
            var filterExprs = FilterDiplomaRequests(teacherId, query);

            Database.ProjectTitles.Get(
                new FilterExpression<ProjectTitle>(t => CultureConfiguration.LocaleNames.Contains(t.Locale.Name) || t.LocaleId == 1), 
                new[] { new IncludeExpression<ProjectTitle>(p => p.Locale) });

            var includePaths = GetIncludeProjectFio();
            var projects = Database.Projects.Get(filterExprs.ToArray(), includePaths.ToArray(),
                currentPage, itemsPerPage, new SortExpression<Project, DateTime>(p => p.CreationDate, ListSortDirection.Descending));

            var projectDtos = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects).ToList();
            SetProjectTitlesToProject(projectDtos);

            return projectDtos;
        }

        private static IEnumerable<FilterExpression<Project>> FilterDiplomaRequests(int teacherId, string query)
        {
            var filterExprs = new List<FilterExpression<Project>>
            {
                new FilterExpression<Project>(p => p.TeacherId == teacherId)
            };
            if (!string.IsNullOrWhiteSpace(query))
            {
                filterExprs.Add(new FilterExpression<Project>(p => p.ProjectTitles.Any(pt =>
                    !string.IsNullOrEmpty(pt.Title) && pt.Title.Contains(query))));
            }
            return filterExprs;
        }

        private void SetProjectTitlesToProject(IEnumerable<ProjectDTO> projectDtos)
        {
            var locales =
                Database.Locales.Get(
                    new FilterExpression<Locale>(l => CultureConfiguration.LocaleNames.Contains(l.Name)));

            var localesDto = Mapper.Map<IEnumerable<Locale>, IEnumerable<LocaleDTO>>(locales).ToList();

            foreach (var projectDto in projectDtos)
            {
                var localeNamesExists = projectDto.ProjectTitles.Select(p => p.Locale.Name);
                var localesRest = localesDto.Where(l => !localeNamesExists.Contains(l.Name));

                foreach (var locale in localesRest)
                {
                    var localeDto = localesDto.FirstOrDefault(l => l.Id == locale.Id);
                    projectDto.ProjectTitles.Add(new ProjectTitleDTO
                    {
                        CreationDate = DateTime.Now,
                        Locale = localeDto
                    });
                }
            }
        }

        private IEnumerable<IncludeExpression<Project>> GetIncludeProjectFio()
        {
            var includePaths = new List<IncludeExpression<Project>>
            {
                new IncludeExpression<Project>(p => p.Student),
                new IncludeExpression<Project>(p => p.Student.Group),
                new IncludeExpression<Project>(p => p.Student.PeopleNames)
            };

            if (!string.IsNullOrWhiteSpace(CultureConfiguration.DefaultLocaleName))
            {
                Database.PeopleNames.Get(
                    new FilterExpression<PeopleName>(f => f.Locale.Name == CultureConfiguration.DefaultLocaleName),
                    new[] {new IncludeExpression<PeopleName>(p => p.Locale)});
            }
            else
            {
                includePaths.Add(new IncludeExpression<Project>(p => p.Student.PeopleNames));
            }
            return includePaths;
        }

        public int GetDiplomaRequestsCount(int teacherId, string query = "")
        {
            return Database.Projects.Count(FilterDiplomaRequests(teacherId, query).ToArray());
        }

        public ProjectEdit EditDiplomaProject(ProjectEdit project)
        {
            var proj = Database.Projects.Get(project.Id);
            proj.CreationDate = DateTime.Now;

            proj.PracticeJournalPassed = project.PracticeJournalPassed;

            Database.Projects.Update(proj);

            var projTitles = project.ProjectTitles.ToList();
            foreach (var projectTitle in projTitles)
            {
                if (projectTitle.Id > 0)
                {
                    var pTitle = Database.ProjectTitles.Get(projectTitle.Id);
                    pTitle.Title = projectTitle.Title;
                    Database.ProjectTitles.Update(pTitle);
                }
                else
                {
                    var pTitle = new ProjectTitle
                    {
                        CreationDate = DateTime.Now,
                        ProjectId = project.Id,
                        LocaleId = projectTitle.LocaleId,
                        Title = projectTitle.Title
                    };
                    Database.ProjectTitles.Add(pTitle);
                }
            }
            Database.Save();

            var projTitlelDb = Database.ProjectTitles.Local.ToList();
            for (var i = 0; i < projTitles.Count; i++)
            {
                projTitles[i].Id = projTitlelDb[i].Id;
            }
            return project;
        }

        public void RespondDiplomaRequest(int projectId, bool? accepted, string comment = null)
        {
            var project = Database.Projects.Get(projectId);
            if (project != null)
            {
                project.Accepted = accepted;
                project.Comment = comment;
                Database.Projects.Update(project);
                Database.Save();

                string body = "EDITING TEST!";
                if (!string.IsNullOrWhiteSpace(comment))
                    body += " " + comment;
                EmailService.SendEmailAsync("teland94@mail.ru", "Test", body);
            }
            else
                throw new InvalidOperationException("Project not found");
        }
    }

    public class ProjectEdit
    {
        public int Id { get; set; }
        public DateTime? PracticeJournalPassed { get; set; }
        public IEnumerable<ProjectEditTitle> ProjectTitles { get; set; }
    }

    public class ProjectEditTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LocaleId { get; set; }
    }
}