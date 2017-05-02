using DiplomaManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.Extensions;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.DAL.Utils;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.DAL.Entities.SharedEntities;

namespace DiplomaManager.BLL.Services
{
    public class AdminService : IAdminService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private ILocaleConfiguration CultureConfiguration { get; }
        private IEmailService EmailService { get; }

        public AdminService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            Database = uow;
            CultureConfiguration = configuration;
            EmailService = emailService;
        }

        public IEnumerable<ProjectDTO> GetProjects(PageInfo pageInfo, ProjectFilter filter)
        {
            List<ProjectDTO> projects = new List<ProjectDTO>();

            List<FilterExpression<Project>> filterExpressions = 
                new List<FilterExpression<Project>>();

            List<IncludeExpression<Project>> includeExpressions = 
                new List<IncludeExpression<Project>>();
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Student));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Student.PeopleNames));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Student.Group));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Student.Group.Degree));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Student.Group.Degree.DegreeNames));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Teacher));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.Teacher.PeopleNames));
            includeExpressions.Add(new IncludeExpression<Project>(p => p.ProjectTitles));
            

            List<SortExpression<Project, object>> sortExpressions = 
                new List<SortExpression<Project, object>>();

            IEnumerable<Project> databaseProjects = new List<Project>();

            // Select by acceptance
            switch (filter.AcceptanceStatus)
            {
                case ProjectAcceptanceStatus.Active:
                    filterExpressions.Add(new FilterExpression<Project>(p => p.Accepted == null));
                    break;

                case ProjectAcceptanceStatus.Refused:
                    filterExpressions.Add(new FilterExpression<Project>(p => p.Accepted == false));
                    break;

                case ProjectAcceptanceStatus.Accepted:
                    filterExpressions.Add(new FilterExpression<Project>(p => p.Accepted == true));
                    break;
            }

            // Select by journal status
            switch(filter.PracticeJournalStatus)
            {
                case PracticeJournalStatus.Passed:
                    filterExpressions.Add(new FilterExpression<Project>(p => p.PracticeJournalPassed != null));
                    break;

                case PracticeJournalStatus.NotPassed:
                    filterExpressions.Add(new FilterExpression<Project>(p => p.PracticeJournalPassed == null));
                    break;
            }

            // Select by student
            if (filter.StudentId != 0)
            {
                filterExpressions.Add(new FilterExpression<Project>(p => p.StudentId == filter.StudentId));
            }

            // Select by teacher
            if (filter.TeacherId != 0)
            {
                filterExpressions.Add(new FilterExpression<Project>(
                    p => p.TeacherId == filter.TeacherId));
            }

            // Select by group
            if (filter.GroupId != 0)
            {
                filterExpressions.Add(new FilterExpression<Project>(
                    p => p.Student.GroupId == filter.GroupId));                
            }

            // Select by degree
            if (filter.DegreeId != 0)
            {
                filterExpressions.Add(new FilterExpression<Project>(p => 
                    p.Student.Group.DegreeId == filter.DegreeId));
            }

            // Select by graduation year
            if (filter.GraduationYear != 0)
            {
                filterExpressions.Add(new FilterExpression<Project>(p => 
                    p.Student.Group.GraduationYear == filter.GraduationYear));                
            }
            
            // Select by project title
            if (!string.IsNullOrEmpty(filter.Title))
            {
                filterExpressions.Add(new FilterExpression<Project>(
                    p => p.ProjectTitles.Any(t => t.Title == filter.Title)));
            }

            // Sort and get projects from database
            switch(filter.SortedField)
            {
                case ProjectSortedField.Acceptance:
                    databaseProjects = 
                        this.GetProjectsSortedByAcceptance(
                            pageInfo, 
                            filter, 
                            includeExpressions.ToArray(), 
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.CreationDate:
                    databaseProjects =
                        this.GetProjectsSortedByCreationDate(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.Degree:
                    databaseProjects =
                        this.GetProjectsSortedByDegree(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.GraduationYear:
                    databaseProjects =
                        this.GetProjectsSortedByGraduationYear(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.Group:
                    databaseProjects =
                        this.GetProjectsSortedByGroup(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.PracticeJournalPassedDate:
                    databaseProjects =
                         this.GetProjectsSortedByJournalDate(
                             pageInfo,
                             filter,
                             includeExpressions.ToArray(),
                             filterExpressions.ToArray());
                    break;

                case ProjectSortedField.Student:
                    databaseProjects =
                        this.GetProjectsSortedByStudent(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;

                case ProjectSortedField.Teacher:
                    sortExpressions.Add(new SortExpression<Project, object>(
                        p => p.Teacher.PeopleNames
                            .Where(pn => 
                                        pn.Locale.Name == CultureConfiguration.DefaultLocaleName && 
                                        pn.NameKind == DAL.Entities.UserEnitites.NameKind.LastName).FirstOrDefault().Name,
                        filter.SortDirection));
                    sortExpressions.Add(new SortExpression<Project, object>(
                        p => p.Teacher.PeopleNames
                            .Where(pn =>
                                        pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                                        pn.NameKind == DAL.Entities.UserEnitites.NameKind.FirstName).FirstOrDefault().Name,
                        filter.SortDirection));
                    sortExpressions.Add(new SortExpression<Project, object>(
                        p => p.Teacher.PeopleNames
                            .Where(pn =>
                                        pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                                        pn.NameKind == DAL.Entities.UserEnitites.NameKind.Patronymic).FirstOrDefault().Name,
                        filter.SortDirection));
                    break;

                case ProjectSortedField.Title:
                    databaseProjects =
                        this.GetProjectsSortedByTitle(
                            pageInfo,
                            filter,
                            includeExpressions.ToArray(),
                            filterExpressions.ToArray());
                    break;
            }

            foreach(Project prj in databaseProjects)
            {
                projects.Add(MapProjectToProjectDTO(prj));
            }

            return projects;
        }

        private ProjectDTO MapProjectToProjectDTO(Project project)
        {
            int localeId = 
                this.Database.Locales.Get(new FilterExpression<Locale>(l => l.Name == CultureConfiguration.DefaultLocaleName))
                    .FirstOrDefault().Id;

            ProjectDTO projectDTO = new ProjectDTO()
            {
                Id = project.Id,
                StudentId = project.StudentId,
                TeacherId = project.TeacherId,

                Accepted = project.Accepted,
                Comment = (string.IsNullOrEmpty(project.Comment)) ? "-" : project.Comment,
                CreationDate = project.CreationDate,
                PracticeJournalPassed = project.PracticeJournalPassed,
                
                ProjectTitles = new List<ProjectTitleDTO>()
            };

            string projectTitle =
                project.ProjectTitles.Where(pt => pt.LocaleId == localeId)
                    .FirstOrDefault()
                    ?.Title;
            projectDTO.ProjectTitles.Add(
                new ProjectTitleDTO()
                {
                    Title = (string.IsNullOrEmpty(projectTitle)) ? "-" : projectTitle
                });

            StudentDTO studentDTO = new StudentDTO();
            studentDTO.PeopleNames = new List<PeopleNameDTO>();
            studentDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Student.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.LastName).FirstOrDefault()?.Name,
                    NameKindDTO.LastName));
            studentDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Student.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.FirstName).FirstOrDefault()?.Name,
                    NameKindDTO.FirstName));
            studentDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Student.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.Patronymic).FirstOrDefault()?.Name,
                    NameKindDTO.Patronymic));

            projectDTO.Student = studentDTO;

            TeacherDTO teacherDTO = new TeacherDTO();
            teacherDTO.PeopleNames = new List<PeopleNameDTO>();
            teacherDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Teacher.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.LastName).FirstOrDefault()?.Name,
                    NameKindDTO.LastName));
            teacherDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Teacher.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.FirstName).FirstOrDefault()?.Name,
                    NameKindDTO.FirstName));
            teacherDTO.PeopleNames.Add(
                MapPeopleNameToPeopleNameDTO(
                    project.Teacher.PeopleNames.Where(
                        pn => pn.LocaleId == localeId &&
                        pn.NameKind == NameKind.Patronymic).FirstOrDefault()?.Name,
                    NameKindDTO.Patronymic));

            projectDTO.Teacher = teacherDTO;
            
            return projectDTO;
        }

        private PeopleNameDTO MapPeopleNameToPeopleNameDTO(string name, NameKindDTO nameKind)
        {
            PeopleNameDTO dto = new PeopleNameDTO();
            dto.NameKind = nameKind;
            dto.Name = (string.IsNullOrEmpty(name)) ? "-" : name;

            return dto;
        }

        private IEnumerable<Project> GetProjectsSortedByAcceptance(
            PageInfo pageInfo, 
            ProjectFilter filter, 
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, bool?> sortExpression =
                new SortExpression<Project, bool?>(p => p.Accepted, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions, 
                includeExpressions, 
                pageInfo.Page, 
                pageInfo.PageSize, 
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByCreationDate(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, DateTime> sortExpression =
                new SortExpression<Project, DateTime>(p => p.CreationDate, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByDegree(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, int> sortExpression =
                new SortExpression<Project, int>(p => p.Student.Group.DegreeId, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByGraduationYear(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, int> sortExpression =
                new SortExpression<Project, int>(p => p.Student.Group.GraduationYear, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByGroup(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, int> sortExpression =
                new SortExpression<Project, int>(p => p.Student.GroupId, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByJournalDate(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, DateTime?> sortExpression =
                new SortExpression<Project, DateTime?>(p => p.PracticeJournalPassed, filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        private IEnumerable<Project> GetProjectsSortedByStudent(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, string>[] sortExpressions =
                new SortExpression<Project, string>[3];

            sortExpressions[0] = new SortExpression<Project, string>(
                p => p.Student.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.LastName).FirstOrDefault().Name,
                filter.SortDirection);
            sortExpressions[1] = new SortExpression<Project, string>(
                p => p.Student.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.FirstName).FirstOrDefault().Name,
                filter.SortDirection);

            sortExpressions[2] = new SortExpression<Project, string>(
                p => p.Student.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.Patronymic).FirstOrDefault().Name,
                filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpressions);
        }

        private IEnumerable<Project> GetProjectsSortedByTeacher(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, string>[] sortExpressions =
                new SortExpression<Project, string>[3];

            sortExpressions[0] = new SortExpression<Project, string>(
                p => p.Teacher.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.LastName).FirstOrDefault().Name,
                filter.SortDirection);
            sortExpressions[1] = new SortExpression<Project, string>(
                p => p.Teacher.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.FirstName).FirstOrDefault().Name,
                filter.SortDirection);

            sortExpressions[2] = new SortExpression<Project, string>(
                p => p.Teacher.PeopleNames.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName &&
                    pn.NameKind == DAL.Entities.UserEnitites.NameKind.Patronymic).FirstOrDefault().Name,
                filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpressions);
        }

        private IEnumerable<Project> GetProjectsSortedByTitle(
            PageInfo pageInfo,
            ProjectFilter filter,
            IncludeExpression<Project>[] includeExpressions,
            FilterExpression<Project>[] filterExpressions)
        {
            SortExpression<Project, string> sortExpression = new SortExpression<Project, string>(
                p => p.ProjectTitles.Where(pn => pn.Locale.Name == CultureConfiguration.DefaultLocaleName)
                    .FirstOrDefault().Title,
                filter.SortDirection);

            return Database.Projects.Get(
                filterExpressions,
                includeExpressions,
                pageInfo.Page,
                pageInfo.PageSize,
                sortExpression);
        }

        public IDictionary<int, string> GetDegrees(ProjectFilter filter)
        {
            IDictionary<int, string> degrees = new Dictionary<int, string>();
            degrees.Add(0, "-");

            /*
            List<FilterExpression<Degree>> filterExpressions = new List<FilterExpression<Degree>>();
            filterExpressions.Add(new FilterExpression<Degree>(
                ));*/

            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetGroups(ProjectFilter filter)
        {
            throw new NotImplementedException();
        } 

        public IDictionary<int, string> GetStudents(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetTeachers(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, string> GetYears(ProjectFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
