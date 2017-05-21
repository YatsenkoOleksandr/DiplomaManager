using AutoMapper;
using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Extensions.Admin;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Modules
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                // Map entities to DTO's

                config.CreateMap<NameKind, NameKindDTO>();
                config.CreateMap<PeopleName, PeopleNameDTO>();
                config.CreateMap<DegreeName, DegreeNameDTO>();
                config.CreateMap<Degree, DegreeDTO>();
                config.CreateMap<Locale, LocaleDTO>();
                config.CreateMap<Group, GroupDTO>();                
                config.CreateMap<User, UserDTO>();
                config.CreateMap<Student, StudentDTO>();
                config.CreateMap<Teacher, TeacherDTO>();
                config.CreateMap<ProjectTitle, ProjectTitleDTO>();
                config.CreateMap<Project, ProjectDTO>();

                // Map DTO's to ViewModels

                config.CreateMap<ProjectDTO, ProjectShortInfo>()
                    .ForMember(
                        d => d.Id,
                        opt => opt.MapFrom(s => s.Id))
                    .ForMember(
                        d => d.Acceptance,
                        opt => opt.MapFrom(s => s.AcceptanceToString()))
                    .ForMember(
                        d => d.CreationalDate,
                        opt => opt.MapFrom(s => s.CreationDate.ToString()))
                    .ForMember(
                        d => d.Degree,
                        opt => opt.MapFrom(s => s.DegreeToString(193)))
                    .ForMember(
                        d => d.GraduationYear,
                        opt => opt.MapFrom(s => s.Student.Group.GraduationYear.ToString()))
                    .ForMember(
                        d => d.Group,
                        opt => opt.MapFrom(s => s.Student.Group.Name))
                    .ForMember(
                        d => d.PracticeJournalPassDate,
                        opt => opt.MapFrom(s => s.PracticeJournalPassDateToString()))
                    .ForMember(
                        d => d.Student,
                        opt => opt.MapFrom(s => s.Student.GetFullName(193)))
                    .ForMember(
                        d => d.Teacher,
                        opt => opt.MapFrom(s => s.Teacher.GetFullName(193)))
                    .ForMember(
                        d => d.Title,
                        opt => opt.MapFrom(s => s.TitleToString(193)));
            });
        }
    }
}
